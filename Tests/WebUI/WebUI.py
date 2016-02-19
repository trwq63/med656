from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as ec


class WebUI:
    driver = webdriver.Firefox()

    def __init__(self,baseurl='http://localhost:25396/'):
        self.baseurl = baseurl

    def go_home(self):
        self.driver.get(self.baseurl)
        return self.driver.page_source

    def get_page(self):
        return self.driver.page_source

    def login(self,user,pwd):
        self.go_home()
        try:
            self.driver.find_element(by='id',value='loginLink').click()
            self.driver.find_element(by='id',value='UserName').send_keys(user)
            self.driver.find_element(by='id',value='Password').send_keys(pwd)
            self.driver.find_element(by='css selector',value='input[type=submit]').click()
        except:
            return False
        return True

    def check_login(self, t=5):
        self.go_home()
        try:
            wait = WebDriverWait(self.driver, t).until(
                ec.presence_of_element_located((By.CSS_SELECTOR, 'a[title=Manage]'))
            )
        except:
            return False
        return True

    def logoff(self):
        self.go_home()
        try:
            self.driver.find_element(by='css selector', value='a[href$=\".submit()\"]').click()
        except:
            return False
        return True

    def check_logoff(self, t=10):
        try:
            wait = WebDriverWait(self.driver, t).until(
                ec.presence_of_element_located((By.ID, 'loginLink'))
            )
        except:
            return False
        return True

    def request_account(self,account_type,user,pwd,email,first_name,last_name,address,phone_number):
        self.driver.get(self.baseurl)
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

    def check_request_account(self):
        if 'Account Confirmation' in self.driver.page_source:
            return True
        return False

    def upload_files(self,files,activity_dict):
        self.go_home()
        try:
            self.driver.find_element(by='link text',value='Upload Data').click()
            self.driver.find_element(by='name',value='files').send_keys(files)
            #for activity in activity_dict:
            #    self.driver.
            self.driver.find_element(by='id',value='btnSubmit').click()
            #wait = WebDriverWait(self.driver,10).until(
            #    ec.presence_of_element_located((By.ID,''))
            #)
        except:
            return False
        return True

    def create_patient(self,user,pwd,email,first_name,last_name,address,phone_number):
        self.driver.get(self.baseurl)
        try:
            self.driver.find_element(by='id',value='requestAccountLink').click()
            self.driver.find_element(by='css selector',value='[for=radioPhysician]').click()
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
