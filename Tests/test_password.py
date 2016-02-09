from WebUI.WebUI import WebUI

web_sess = WebUI()

def test_password():
    user = 'pjfry'
    pwd = 'P@ssword10'
    email = 'pjfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    try:
        web_sess.request_account('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    except:
        pass
    assert web_sess.check_request_account()

def test_password_length():
    user = 'pfry'
    pwd = 'P@ssword1'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    try:
        web_sess.request_account('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    except:
        pass
    assert 'Passwords must be at least 10 characters long' in web_sess.get_page()

def test_password_case():
    user = 'pfry'
    pwd = 'p@ssword10'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    try:
        web_sess.request_account('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    except:
        pass
    assert 'Passwords must have at least one uppercase' in web_sess.get_page()

def test_password_digit():
    user = 'pfry'
    pwd = 'P@ssworddd'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    try:
        web_sess.request_account('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    except:
        pass
    assert 'Passwords must have at least one digit' in web_sess.get_page()

def test_password_special_char():
    user = 'pfry'
    pwd = 'Password10'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    try:
        web_sess.request_account('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    except:
        pass
    assert 'Passwords must have at least one non letter or digit character' in web_sess.get_page()
