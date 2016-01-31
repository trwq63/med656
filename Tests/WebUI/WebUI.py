from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

class WebUI:
    driver = webdriver.Firefox()

    def __init__(self,baseurl='http://localhost:25396/',user='test@uah.com',pwd='P@ssword1'):
        self.user = user
        self.pwd = pwd
        self.baseurl = baseurl

    def login(self,user='',pwd=''):
        if user == '':
            user = self.user
        if pwd == '':
            pwd = self.pwd

        self.driver.get(self.baseurl)
        elem = self.driver.find_element(by='id',value='loginLink')
        elem.click()
        elem = self.driver.find_element(by='id',value='Email')
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

    def register_user(self,user='',pwd=''):
        if user == '':
            user = self.user
        if pwd == '':
            pwd = self.pwd

        self.driver.get(self.baseurl)
        elem = self.driver.find_element(by='id',value='registerLink')
        elem.click()
        elem = self.driver.find_element(by='id',value='Email')
        elem.send_keys(user)
        elem = self.driver.find_element(by='id',value='Password')
        elem.send_keys(pwd)
        elem = self.driver.find_element(by='id',value='ConfirmPassword')
        elem.send_keys(pwd)
        elem.submit()
        return self.driver.page_source
