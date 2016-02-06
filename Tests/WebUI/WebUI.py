from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

class WebUI:
    driver = webdriver.Firefox()

    def __init__(self,baseurl='http://localhost:25396/'):
        self.baseurl = baseurl

    def login(self,user,pwd):
        self.driver.get(self.baseurl)
        elem = self.driver.find_element(by='id',value='loginLink')
        elem.click()
        elem = self.driver.find_element(by='id',value='UserName')
        elem.send_keys(user)
        elem = self.driver.find_element(by='id',value='Password')
        elem.send_keys(pwd)
        elem.submit()
        return self.driver.page_source

    def logoff(self):
        #self.driver.get(self.baseurl)
        elem = self.driver.find_element(by='link text',value='Log off')
        elem.click()
        wait = WebDriverWait(self.driver,10).until(
            EC.presence_of_element_located((By.ID,'loginLink'))
        )
        return self.driver.page_source

    def request_account(self,account_type,user,pwd,email,first_name,last_name,address,phone_number):
        self.driver.get(self.baseurl)
        self.driver.find_element(by='id',value='requestAccountLink').click()
        if account_type == 'Physician':
            self.driver.find_element(by='id',value='radioPhysician').click()
        elif account_type == 'Exp Admin':
            self.driver.find_element(by='id',value='radioExpAdmin').click()
        self.driver.find_element(by='id',value='FirstName').send_keys(fist_name)
        self.driver.find_element(by='id',value='LastName').send_keys(last_name)
        self.driver.find_element(by='id',value='Email').send_keys(email)
        self.driver.find_element(by='id',value='Username').send_keys(user)
        self.driver.find_element(by='id',value='Password').send_keys(pwd)
        self.driver.find_element(by='id',value='ConfirmPassword').send_keys(pwd)
        self.driver.find_element(by='id',value='Address').send_keys(address)
        self.driver.find_element(by='id',value='PhoneNumber').send_keys(phone_number)
        self.driver.find_element(by='id',value='ReasonForAccount').send_keys('test').submit()
        return self.driver.page_source
