from WebUI.WebUI import WebUI

web_sess = WebUI()

def test_username():
    user = 'copy'
    pwd = 'P@ssword10'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    # first use of user copy
    retVal = web_sess.register_user('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    assert 'Hello '+str(user)+'!' in retVal

    # second use of user copy
    retVal = web_sess.register_user('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    assert 'User already exists' in retVal

    # first use of user copy1
    retVal = web_sess.register_user('Physician','copy1',pwd,email,first_name,last_name,address,phone_number)
    assert 'Hello '+str(user)+'!' in retVal
