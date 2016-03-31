"""
These are the web user interface utilities used to drive the web tests.
"""
from selenium import webdriver
from selenium.webdriver.support import select
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as ec
import sys


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

    def upload_files(self, files, activity_dict):
        """
        This will uplaod files for a patient with the given activities

        :param files: Relative paths to files to be uploaded
        :param activity_dict: A dictionary with index of activity and elements of stime and ftime
        :return:
        """
        try:
            self.driver.find_element(by='link text', value='Upload Data').click()
            self.driver.find_element(by='name', value='files').send_keys(files)
            for activity in activity_dict:
                pass
            self.driver.find_element_by_css_selector('input[type=submit]').click()
        except:
            return False
        return True

    def create_patient(self, phy, phy_pass, user, pwd, bday, location, weight, height, gender, race, ethnicity):
        """
        This will create a patient account. It expects the physician to be logged in when called.

        :param user:
        :param pwd:
        :return:
        """
        if not self.check_login(phy, t=1):
            self.login(phy, phy_pass)
        try:
            self.driver.find_element_by_css_selector('button[onclick=createPatient\(\)]').click()
            self.driver.find_element_by_id('Username').send_keys(user)
            self.driver.find_element_by_id('Password').send_keys(pwd)
            self.driver.find_element_by_id('Birthdate').click()
            # temporary work around
            self.driver.find_element_by_css_selector('button[class=picker__button--today').click()
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
                # workaround: ethnicity misspelling
                self.driver.find_element_by_css_selector('label[for=enthicityNonHispanic]').click()
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
            self.driver.find_element_by_xpath("//tr/td[text()='{}']/../td/button[contains(text(),'Delete Patient')]".format(user)).click()
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
                self.driver.find_element_by_id('Email').send_keys(email)
            if address != '':
                self.driver.find_element_by_id('Address').send_keys(address)
            if phonenum != '':
                self.driver.find_element_by_id('PhoneNumber').send_keys(phonenum)
            self.driver.find_element_by_css_selector('input[type=submit]').click()
            self.driver.find_element_by_css_selector('input[type=submit]').click()
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
                tmp = self.driver.find_element_by_id('Email').value_of_css_property('value')
                print(tmp)
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