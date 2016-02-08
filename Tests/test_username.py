from WebUI.WebUI import WebUI

web_sess = WebUI()

def test_username_cannot_be_coppied():
    user = 'copy'
    pwd = 'P@ssword10'
    email = 'copy@copy.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    # first use of user copy
    retVal = web_sess.request_account('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    assert web_sess.check_request_account()

    # second use of user copy
    retVal = web_sess.request_account('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    assert 'User already exists' in retVal

    # first use of user copy1
    retVal = web_sess.request_account('Physician','copy1',pwd,'copy1@copy.com',first_name,last_name,address,phone_number)
    assert web_sess.check_request_account()
