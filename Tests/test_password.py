"""
These tests are to make sure password functionality meets the requirements
"""
from WebUI.WebUI import WebUI
from time import sleep

web_sess = WebUI()


def test_password_length(logoff):
    """
    **Requirements:**

    - 3.1.1.4: System requires a minimum security criteria
    - 3.1.1.4.1: Password must be at least 10 characters

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'pfry'
    - pwd = 'P@ssword1'
    - email = 'pfry@futurama.com'
    - first_name = 'Fry'
    - last_name = 'Phillip'
    - address = '304 Wherever Street, New New York City, New New York'
    - phone_number = '123-456-7890'


    ===============  =====================  =============
    Steps            Expected Result        Actual Result
    ===============  =====================  =============
    request account  password length error
    ===============  =====================  =============
    """
    print('Starting')

    user = 'pfry'
    pwd = 'P@ssword1'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    print('Requesting account for user ', user,
          'pwd: ', pwd,
          'email: ', email,
          'first_name: ', first_name,
          'last_name: ', last_name,
          'address: ', address,
          'phone_number: ', phone_number)
    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    sleep(3)
    assert 'The Password must be at least 10 characters long.' in web_sess.get_page()


def test_password_case(logoff):
    """
    **Requirements:**

    - 3.1.1.4: System requires a minimum security criteria
    - 3.1.1.4.2: Password shall contain at least 1 upper and 1 lower case character

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'pfry'
    - pwd = 'p@ssword10'
    - email = 'pfry@futurama.com'
    - first_name = 'Fry'
    - last_name = 'Phillip'
    - address = '304 Wherever Street, New New York City, New New York'
    - phone_number = '123-456-7890'


    ===============  =====================  =============
    Steps            Expected Result        Actual Result
    ===============  =====================  =============
    request account  password case error
    ===============  =====================  =============
    """
    print('Starting')

    user = 'pfry'
    pwd = 'p@ssword10'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    print('Requesting account for user ', user,
          'pwd: ', pwd,
          'email: ', email,
          'first_name: ', first_name,
          'last_name: ', last_name,
          'address: ', address,
          'phone_number: ', phone_number)
    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    assert 'Passwords must have at least one uppercase' in web_sess.get_page()


def test_password_digit(logoff):
    """
    **Requirements:**

    - 3.1.1.4: System requires a minimum security criteria
    - 3.1.1.4.3: Password shall contain at least 1 number

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'pfry'
    - pwd = 'P@ssworddd'
    - email = 'pfry@futurama.com'
    - first_name = 'Fry'
    - last_name = 'Phillip'
    - address = '304 Wherever Street, New New York City, New New York'
    - phone_number = '123-456-7890'


    ===============  =====================  =============
    Steps            Expected Result        Actual Result
    ===============  =====================  =============
    request account  password digit error
    ===============  =====================  =============
    """
    print('Starting')

    user = 'pfry'
    pwd = 'P@ssworddd'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    print('Requesting account for user ', user,
          'pwd: ', pwd,
          'email: ', email,
          'first_name: ', first_name,
          'last_name: ', last_name,
          'address: ', address,
          'phone_number: ', phone_number)
    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    assert 'Passwords must have at least one digit' in web_sess.get_page()


def test_password_special_char(logoff):
    """
    **Requirements:**

    - 3.1.1.4: System requires a minimum security criteria
    - 3.1.1.4.4: Password shall contain at least 1 special character

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'pfry'
    - pwd = 'Password10'
    - email = 'pfry@futurama.com'
    - first_name = 'Fry'
    - last_name = 'Phillip'
    - address = '304 Wherever Street, New New York City, New New York'
    - phone_number = '123-456-7890'


    ===============  =====================  =============
    Steps            Expected Result        Actual Result
    ===============  =====================  =============
    request account  password digit error
    ===============  =====================  =============
    """
    print('Starting')

    user = 'pfry'
    pwd = 'Password10'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    print('Requesting account for user ', user,
          'pwd: ', pwd,
          'email: ', email,
          'first_name: ', first_name,
          'last_name: ', last_name,
          'address: ', address,
          'phone_number: ', phone_number)
    web_sess.request_account('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    assert 'Passwords must have at least one non letter or digit character' in web_sess.get_page()


def test_reset_password(login_sysadmin):
    """
    **Requirements:**

    - 3.1.1.1.4.3: System Administrators can reset passwords

    **Pre Conditions:**

    - login_sysadmin fixture

    **Input:**

    - user = 'testPhysician2'
    - username = 'test physician2'
    - pwd = 'Password1!'


    ==================================  =====================  =============
    Steps                               Expected Result        Actual Result
    ==================================  =====================  =============
    Set users password to new password  No errors
    Login with the new password         User can login
    ==================================  =====================  =============
    """
    user = 'testPhysician2'
    username = 'test physician2'
    pwd = 'Password1!'

    assert web_sess.reset_user_password(username, pwd)
    web_sess.login(user, pwd)
    assert web_sess.check_login()
