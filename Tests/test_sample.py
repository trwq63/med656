from selenium import webdriver
from selenium.webdriver.common.keys import Keys

def sample():
    driver = webdriver.Firefox()
    driver.get("http://www.python.org")
    assert "Python" in driver.title
    elem = driver.find_element_by_name("q")
    elem.send_keys("pycon")
    elem.send_keys(Keys.RETURN)
    return driver.page_source
    driver.close()

def test_sample():
    assert "no results found." not in sample()
