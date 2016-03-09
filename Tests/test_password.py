"""
These tests are to make sure password functionality meets the requirements
"""
from WebUI.WebUI import WebUI
from time import sleep

web_sess = WebUI()

'''
Removed this test because it overlaps with other portions of the automated testing

def test_good_password(logoff):
    """
    This test tries to create an account with a password that meets all the requirements
    Validation is done by checking the web page's response after submitting the request

    :param logoff: Makes sure the session is in a logged off state before the test starts
    :return:
    """
    user = 'pjfry'
    pwd = 'P@ssword10'
    email = 'pjfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    status = web_sess.check_request_account()
    assert status == True
'''


def test_password_length(logoff):
    """
    This test tries to create an account with a password that is too short
    Validation is done by checking the web page's response after submitting the request

    :param logoff: Makes sure the session is in a logged off state before the test starts
    :return:
    """
    user = 'pfry'
    pwd = 'P@ssword1'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    sleep(3)
    assert 'The Password must be at least 10 characters long.' in web_sess.get_page()


def test_password_case(logoff):
    """
    This test tries to create an account with a password that is has no uppercase
    Validation is done by checking the web page's response after submitting the request

    :param logoff: Makes sure the session is in a logged off state before the test starts
    :return:
    """
    user = 'pfry'
    pwd = 'p@ssword10'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    assert 'Passwords must have at least one uppercase' in web_sess.get_page()


def test_password_digit(logoff):
    """
    This test tries to create an account with a password that has no numbers
    Validation is done by checking the web page's response after submitting the request

    :param logoff: Makes sure the session is in a logged off state before the test starts
    :return:
    """
    user = 'pfry'
    pwd = 'P@ssworddd'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    assert 'Passwords must have at least one digit' in web_sess.get_page()


def test_password_special_char(logoff):
    """
    This test tries to create an account with a password that is too short
    Validation is done by checking the web page's response after submitting the request

    :param logoff: Makes sure the session is in a logged off state before the test starts
    :return:
    """
    user = 'pfry'
    pwd = 'Password10'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    web_sess.request_account('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    assert 'Passwords must have at least one non letter or digit character' in web_sess.get_page()


def test_reset_password(login_sysadmin):
    """
    This test has the system admin reset a users password. Then the user tries to login with the new password
    Validation is done on the reset step and the login step

    :param login_sysadmin:
    :return:
    """
    user = 'testPhysician2'
    username = 'test physician2'
    pwd = 'Password1!'

    assert web_sess.reset_user_password(username, pwd)
    web_sess.login(user, pwd)
    assert web_sess.check_login()
