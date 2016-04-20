"""
These are the web user interface utilities used to drive the web tests.
"""
from selenium import webdriver
from selenium.webdriver.support import select
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as ec
from selenium.common.exceptions import NoSuchElementException as NoSuchElementException
from selenium.webdriver.support.ui import Select
import sys
import time


class WebUI:
    """
    This class keeps track of the web session. It uses Firefox as the web driver. Its baseurl is the start page of
    the software. By default it is the localhost on port 25396.
    """
    driver = webdriver.Firefox()

    def __init__(self,baseurl='http://localhost:25396/'):
        """
        Initializes the WebUI object. sets the baseurl to http://localhost:25396/ by default.

        :param baseurl: The starting page of the web application
        :return:
        """
        self.baseurl = baseurl
        self.go_home()
        self.driver.maximize_window()

    def go_home(self):
        """
        Navigates the web session back to the base url

        :return:
        """
        self.driver.get(self.baseurl)
        return self.driver.page_source

    def get_page(self):
        """
        Helper procedure to return the current web pages source useful to validate responses on the page

        :return:
        """
        return self.driver.page_source

    def login(self, user, pwd):
        """
        This will make sure you are logged in as the user specified. It will log you out if you are already logged in.

        :param user: Username to log in as
        :param pwd: Password for the given username
        :return:
        """
        if self.check_login(t=1):
            self.logoff()
        try:
            self.driver.find_element(by='id',value='loginLink').click()
            self.driver.find_element(by='id',value='UserName').send_keys(user)
            self.driver.find_element(by='id',value='Password').send_keys(pwd)
            self.driver.find_element(by='css selector',value='input[type=submit]').click()
        except:
            return False
        return True

    def check_login(self, user='', t=5):
        """
        This will verify you are logged in. It checks for the manage account link that only appears if you are
        logged in.

        :param t: A timeout in seconds to wait for the management link to appear
        :param user: Check to see if this is the user that is logged in
        :return:
        """
        try:
            WebDriverWait(self.driver, t).until(
                ec.presence_of_element_located((By.CSS_SELECTOR, 'a[title=Manage]'))
            )
            if user != '':
                return user in self.driver.find_element_by_css_selector('a[title=Manage]').text
        except:
            return False
        return True

    def logoff(self):
        """
        This will log the logged in user off.

        :return:
        """
        try:
            self.driver.find_element(by='css selector', value='a[href$=\".submit()\"]').click()
        except:
            return False
        return True

    def check_logoff(self, t=10):
        """
        This will check that there is no logged in user for the session

        :param t: time in seconds to wait for the login button to appear. The default is 10.
        :return:
        """
        try:
            WebDriverWait(self.driver, t).until(
                ec.presence_of_element_located((By.ID, 'loginLink'))
            )
        except:
            return False
        return True

    def request_account(self, account_type, user, pwd, email, first_name, last_name, address, phone_number):
        """
        This will fill out a request for an account. To request an account you must be logged out therefore
        this procedure will make sure you are logged out

        :param account_type: The type of account to request. can either be Physician or Exp Admin
        :param user: The username of the new account
        :param pwd: The password of the new account
        :param email: The email address of the new account
        :param first_name: The first name of the user
        :param last_name: The last name of the user
        :param address: The home address of the user
        :param phone_number: The phone number of the user
        :return:
        """
        if not self.check_logoff(t=1):
            self.logoff()
        try:
            self.driver.find_element(by='id',value='requestAccountLink').click()
            if account_type == 'Physician':
                self.driver.find_element(by='css selector',value='[for=radioPhysician]').click()
            elif account_type == 'Exp Admin':
                self.driver.find_element(by='css selector',value='[for=radioExpAdmin]').click()
            self.driver.find_element(by='id',value='FirstName').send_keys(first_name)
            self.driver.find_element(by='id',value='LastName').send_keys(last_name)
            self.driver.find_element(by='id',value='Email').send_keys(email)
            self.driver.find_element(by='id',value='Username').send_keys(user)
            self.driver.find_element(by='id',value='Password').send_keys(pwd)
            self.driver.find_element(by='id',value='ConfirmPassword').send_keys(pwd)
            self.driver.find_element(by='id',value='Address').send_keys(address)
            self.driver.find_element(by='id',value='PhoneNumber').send_keys(phone_number)
            self.driver.find_element(by='id',value='ReasonForAccount').send_keys('test')
            self.driver.find_element(by='css selector',value='input[type=submit]').click()
        except:
            return False
        return True

    def approve_account(self, user):
        """
        Logs in as the system admin and approves an account with the given username

        :param username: Username of the account to approve
        :return:
        """
        self.go_home()
        try:
            if not self.check_login(user='fitadmin'):
                self.login('fitadmin','Password1!')
            # find the account to approve and click the approve button
            self.driver.find_element_by_xpath("//tr/td[text()='{}']/../td/button[text()='Approve']".format(user)).click()
        except:
            return False
        return True

    def check_request_account(self):
        """
        Validates that the account request was successful

        :return:
        """
        if 'Account Confirmation' in self.driver.page_source:
            return True
        return False

    def upload_files(self, file, fyear, fmonth, fday, tyear, tmonth, tday, device, activities):
        """
        This will uplaod files for a patient with the given activities

        :param file: Relative paths to files to be uploaded
        :param fyear: The year the file started collecting
        :param fmonth: The month the file started collecting
        :param fday: The day the file started collecting
        :param tyear: The year the file stopped collecting
        :param tmonth: The month the file stopped collecting
        :param tday: The day the file stopped collecting
        :param device: The device the file came from. must be 'zephyr', 'basis', or 'band'
        :param activities: A list of dictionaries. dicts look like: {'type':'Home Eating', 'year':1920, 'month':'January', 'day':3, 'startTime': '11:00 AM', 'endTime': '11:30 AM'}
        :return:
        """
        try:
            self.driver.find_element(by='link text', value='Upload Data').click()
            self.driver.find_element_by_css_selector('input[name="Files"]').send_keys(file)
            self.driver.find_element_by_css_selector('input[id="FromDate"]').click()
            self.driver.find_element_by_xpath('//select[@title="Select a year"]/option[text()="{}"]'.format(fyear)).click()
            self.driver.find_element_by_xpath('//select[@title="Select a month"]/option[text()="{}"]'.format(fmonth)).click()
            self.driver.find_element_by_xpath('//tbody/tr/td/div[@class="picker__day picker__day--infocus"][text()="{}"]'.format(fday)).click()
            time.sleep(5)
            self.driver.find_element_by_css_selector('input[id="ToDate"]').click()
            self.driver.find_element_by_xpath('//select[@title="Select a year"][@aria-controls="ToDate_table"]/option[text()="{}"]'.format(tyear)).click()
            self.driver.find_element_by_xpath('//select[@title="Select a month"][@aria-controls="ToDate_table"]/option[text()="{}"]'.format(tmonth)).click()
            self.driver.find_element_by_xpath('//table[@id="ToDate_table"]/tbody/tr/td/div[@class="picker__day picker__day--infocus"][text()="{}"]'.format(tday)).click()
            time.sleep(5)
            if device == 'zephyr':
                self.driver.find_element_by_css_selector('label[for="device0"]').click()
            elif device == 'basis':
                self.driver.find_element_by_css_selector('label[for="device1"]').click()
            elif device == 'band':
                self.driver.find_element_by_css_selector('label[for="device2"]').click()


            n = 0
            for activity in activities:
                self.driver.find_element_by_xpath('//div/select[@name="Activities[{}].ActivityType"]/../input[@value="Select Activity Type"]'.format(n)).click()
                self.driver.find_element_by_xpath('//div/select[@name="Activities[{}].ActivityType"]/../ul/li/span[text()="{}"]'.format(n, activity['type'])).click()
                self.driver.switch_to.parent_frame()
                time.sleep(2)
                self.driver.find_element_by_css_selector('input[name="Activities[{}].ActivityDate"]'.format(n)).click()
                self.driver.find_element_by_xpath('//div/input[@name="Activities[{}].ActivityDate"]/..//select[@title="Select a year"]/option[@value="{}"]'.format(n,activity['year'])).click()
                self.driver.find_element_by_xpath('//div/input[@name="Activities[{}].ActivityDate"]/..//select[@title="Select a month"]/option[text()="{}"]'.format(n,activity['month'])).click()
                self.driver.find_element_by_xpath('//div/input[@name="Activities[{}].ActivityDate"]/..//table/tbody/tr/td/div[@class="picker__day picker__day--infocus"][text()="{}"]'.format(n, activity['day'])).click()
                time.sleep(5)
                self.driver.find_element_by_xpath('//input[@name="Activities[{}].StartTime"]'.format(n)).click()
                self.driver.find_element_by_xpath('//div/input[@name="Activities[{}].StartTime"]/../div[@id="StartTime[]_root"]/div/div/div/div/ul/li[text()="{}"]'.format(n, activity['startTime'])).click()
                time.sleep(2)
                self.driver.find_element_by_xpath('//input[@name="Activities[{}].EndTime"]'.format(n)).click()
                self.driver.find_element_by_xpath('//div/input[@name="Activities[{}].EndTime"]/../div[@id="EndTime[]_root"]/div/div/div/div/ul/li[text()="{}"]'.format(n, activity['endTime'])).click()
                time.sleep(2)
                n += 1
                # add another activity
                if n < len(activities):
                    self.driver.find_element_by_css_selector('button[class="btn-floating green btn-add"]').click()

            self.driver.find_element_by_xpath('//button[@id="btnSubmit"]').click()

        except Exception as e:
            print('Generated exception: ', e)
            return False
        return True

    def create_patient(self, phy, phy_pass, user, pwd, byear, bmonth, bday, location, weight, height, gender, race, ethnicity):
        """
        This will create a patient account. It expects the physician to be logged in when called.

        :param user:
        :param pwd:
        :return:
        """
        if not self.check_login(phy, t=1):
            self.login(phy, phy_pass)
        try:
            self.driver.find_element_by_css_selector('button[onclick="createPatient()"]').click()
            self.driver.find_element_by_id('Username').send_keys(user)
            self.driver.find_element_by_id('Password').send_keys(pwd)
            self.driver.find_element_by_id('Birthdate').click()
            self.driver.find_element_by_xpath('//select[@title="Select a year"]/option[text()="{}"]'.format(byear)).click()
            self.driver.find_element_by_xpath('//select[@title="Select a month"]/option[text()="{}"]'.format(bmonth)).click()
            self.driver.find_element_by_xpath('//table[@id="Birthdate_table"]/tbody/tr/td/div[@class="picker__day picker__day--infocus"][text()="{}"]'.format(bday)).click()
            select.Select(self.driver.find_element_by_id('Location')).select_by_visible_text(location)
            self.driver.find_element_by_id('Weight').send_keys(weight)
            self.driver.find_element_by_id('Height').send_keys(height)
            if gender == 'male':
                self.driver.find_element_by_css_selector('label[for=genderMale]').click()
            elif gender == 'female':
                self.driver.find_element_by_css_selector('label[for=genderFemale]').click()
            else:
                return False
            if race == 'american_indian':
                self.driver.find_element_by_css_selector('label[for=raceAmericanIndian]').click()
            elif race == 'asian':
                self.driver.find_element_by_css_selector('label[for=raceAsian]').click()
            elif race == 'black':
                self.driver.find_element_by_css_selector('label[for=raceBlack]').click()
            elif race == 'hawaiian':
                self.driver.find_element_by_css_selector('label[for=raceHawaiian]').click()
            elif race == 'white':
                self.driver.find_element_by_css_selector('label[for=raceWhite]').click()
            elif race == 'other':
                self.driver.find_element_by_css_selector('label[for=raceOther]').click()
            else:
                return False
            if ethnicity == 'non_hispanic':
                self.driver.find_element_by_css_selector('label[for="ethnicityNonHispanic"]').click()
            elif ethnicity == 'hispanic':
                self.driver.find_element_by_css_selector('label[for=ethnicityHispanic]').click()
            else:
                return False
            self.driver.find_element_by_css_selector('button[type=submit]').click()
        except:
            e = sys.exc_info()
            print (e)
            return False
        return True

    def check_create_patient(self):
        """
        Validates that the patient creation was successful

        :return:
        """
        if 'Account Confirmation' in self.driver.page_source:
            return True
        return False

    def delete_account(self, user):
        """
        This logs in as the system admin and deletes the specified user

        :param user: username of the account to delete
        :return:
        """
        try:
            if self.check_login():
                self.logoff()
            self.login('fitadmin', 'Password1!')
            self.driver.find_element(by='css selector', value='a[href$=\"/Admin/ManageUsers\"]').click()
            self.driver.find_element_by_xpath("//tr/td[text()='{}']/../td/button/i[text()='delete']/..".format(user)).click()

        except:
            return False
        return True

    def delete_patient(self, phy, phy_pass, user):
        """
        This logs in as the physician and deletes the specified user

        :param phy: name of the physician that the patient belongs to
        :param phy_pass: password of the physician
        :param user: username of the account to delete
        :return:
        """
        try:
            if not self.check_login(phy, t=1):
                self.login(phy, phy_pass)
            self.driver.find_element_by_xpath("//tr/td[text()='{}']/../td/button[contains(text(),'Disable Patient')]".format(user)).click()
            self.driver.find_element_by_css_selector('input[type=submit]').click()
        except:
            return False
        return True

    def set_account_info(self, pwd='', current_pwd='', email='', address='', phonenum=''):
        """
        This procedure will set the account info to any info provided if provided
        It assumes the user is already logged in

        :param pwd: the new password desired
        :param current_pwd: the current password. needed to change passwords
        :param email: the new email address
        :param address: the new home address
        :param phonenum: the new phone number
        :return:
        """
        try:
            self.driver.find_element_by_css_selector('a[title=Manage]').click()
            if pwd != '' and current_pwd != '':
                self.driver.find_element_by_css_selector('a[href=/Manage/ChangePassword]').click()
                self.driver.find_element_by_id('OldPassword').send_keys(current_pwd)
                self.driver.find_element_by_id('NewPassword').send_keys(pwd)
                self.driver.find_element_by_id('ConfirmPassword').send_keys(pwd)
                self.driver.find_element_by_css_selector('input[type=submit]').click()
            if email != '':
                self.driver.find_element_by_id('Email').clear()
                self.driver.find_element_by_id('Email').send_keys(email)
            if address != '':
                self.driver.find_element_by_id('Address').send_keys(address)
            if phonenum != '':
                self.driver.find_element_by_id('PhoneNumber').send_keys(phonenum)
            self.driver.find_element_by_css_selector('input[type="submit"]').click()
            self.driver.find_element_by_css_selector('input[type="submit"]').click()
        except:
            e = sys.exc_info()
            print (e)
            return False
        return True

    def confirm_account_info(self, email='', address='', phonenum=''):
        """
        This procedure will confirm account information of whatever information is provided.

        :param email: the expected email address
        :param address: the expected home address
        :param phonenum: the expected phone number
        :return:
        """
        try:
            self.driver.find_element_by_css_selector('a[title=Manage]').click()
            if email != '':
                tmp = self.driver.find_element_by_id('Email').get_attribute('value')
                print('this is the email: (', tmp, ')')
                if tmp != email:
                    return False
            if address != '':
                tmp = self.driver.find_element_by_id('Address').text
                print(tmp)
                if tmp != address:
                    return False
            if phonenum != '':
                tmp = self.driver.find_element_by_id('PhoneNumber').text
                print(tmp)
                if tmp != phonenum:
                    return False
        except:
            e = sys.exc_info()
            print (e)
            return False
        return True

    def reset_user_password(self, user, new_pwd):
        """
        This procedure will make the system admin reset an accounts password

        :param user: username of the account to touch
        :param new_pwd: the new password to give the account
        :return:
        """
        self.driver.get(self.baseurl)
        try:
            if self.check_login():
                self.logoff()
            self.login('fitadmin', 'Password1!')
            self.driver.find_element(by='css selector', value='a[href$=\"/Admin/ManageUsers\"]').click()
            self.driver.find_element_by_xpath("//tr/td[text()='{}']/../td/button[3]".format(user)).click()
            self.driver.find_element_by_id('Password').send_keys(new_pwd)
            self.driver.find_element_by_id('ConfirmPassword').send_keys(new_pwd)
            self.driver.find_element_by_css_selector('input[type=submit]').click()
        except:
            e = sys.exc_info()
            print (e)
            return False
        return True

    def delete_all_accounts(self):
        if self.check_login():
            self.logoff()
        self.login('fitadmin', 'Password1!')
        self.driver.find_element(by='css selector', value='a[href$=\"/Admin/ManageUsers\"]').click()
        users_exist = True
        while users_exist:
            try:
                self.driver.find_element_by_css_selector('[id*="btnDelete"]').click()
                time.sleep(2)
            except NoSuchElementException:
                users_exist = False


    def create_experiment(self, expName, startAge, endAge, startHght, endHght, startWght, endWght, genders, races, ethnicities, locations):
        try:
            self.driver.find_element_by_link_text('Create Experiment').click()
            self.driver.find_element_by_id('ExperimentName').send_keys(expName)
            select = Select(self.driver.find_element_by_id('ageRangeStart'))
            select.select_by_visible_text(startAge)
            select = Select(self.driver.find_element_by_id('ageRangeEnd'))
            select.select_by_visible_text(endAge)
            select = Select(self.driver.find_element_by_id('heightRangeBegin'))
            select.select_by_visible_text(startHght)
            select = Select(self.driver.find_element_by_id('heightRangeEnd'))
            select.select_by_visible_text(endHght)
            select = Select(self.driver.find_element_by_id('weightRangeBegin'))
            select.select_by_visible_text(startWght)
            select = Select(self.driver.find_element_by_id('weightRangeEnd'))
            select.select_by_visible_text(endWght)
            if 'male' in genders:
                self.driver.find_element_by_css_selector('label[for="gendersMale"]').click()
            if 'female' in genders:
                self.driver.find_element_by_css_selector('label[for="gendersFemale"]').click()
            if 'american_indian' in races:
                self.driver.find_element_by_css_selector('label[for="raceAmericanIndian"]').click()
            if 'asian' in races:
                self.driver.find_element_by_css_selector('label[for="raceAsian"]').click()
            if 'black' in races:
                self.driver.find_element_by_css_selector('label[for="raceBalck"]').click()
            if 'hawaiian' in races:
                self.driver.find_element_by_css_selector('label[for="raceHawaiian"]').click()
            if 'white' in races:
                self.driver.find_element_by_css_selector('label[for="raceWhite"]').click()
            if 'other' in races:
                self.driver.find_element_by_css_selector('label[for="raceOther"]').click()
            if 'non_hispanic' in ethnicities:
                self.driver.find_element_by_css_selector('label[for="ethnicityNonHispanic"]').click()
            if 'hispanic' in ethnicities:
                self.driver.find_element_by_css_selector('label[for="ethnicityHispanic"]').click()
            for location in locations:
                self.driver.find_element_by_css_selector('label[for="location{}"]'.format(location)).click()
            self.driver.find_element_by_css_selector('input[type="submit"]').click()
        except Exception as e:
            print('Generated exception: ', e)
            return False
        return True


    def view_experiment(self, name):
        try:
            self.driver.find_element_by_link_text('View Experiments').click()
            self.driver.find_element_by_xpath('//div[text()="{}"]/../div/input[@value="View"]'.format(name)).click()
        except Exception as e:
            print('Generated exception', e)
            return False
        return True


    def delete_experiment(self, name):
        try:
            self.driver.find_element_by_link_text('View Experiments').click()
            self.driver.find_element_by_xpath('//div[text()="{}"]/../div/input[@value="Delete"]'.format(name)).click()
            self.driver.find_element_by_css_selector('input[Value="Confirm"]').click()
        except Exception as e:
            print('Generated exception', e)
            return False
        return True


    def create_admin(self, user, pwd, email):
        try:
            self.driver.find_element_by_link_text('Create Admin').click()
            self.driver.find_element_by_id('Username').send_keys(user)
            self.driver.find_element_by_id('Password').send_keys(pwd)
            self.driver.find_element_by_id('ConfirmPassword').send_keys(pwd)
            self.driver.find_element_by_id('Email').send_keys(email)
            self.driver.find_element_by_css_selector('input[type="submit"]').click()
        except Exception as e:
            print('Generated exception', e)
            return False
        return True

    def view_data(self):
        try:
            self.driver.find_element_by_css_selector('input[value="View Data"]').click()
        except Exception as e:
            print('Generated exception', e)
            return False
        return True

    def export_data(self):
        try:
            buttons = self.driver.find_elements_by_xpath('//button[text()="Export"]')
            for b in buttons:
                b.click()
        except Exception as e:
            print('Generated exception', e)
            return False
        return True