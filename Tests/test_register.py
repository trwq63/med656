from WebUI.WebUI import WebUI

web_sess = WebUI()

def test_register_physician():
    user = 'pfry'
    pwd = 'P@ssword10'
    email = 'pfry@futurama.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    try:
        retVal = web_sess.register_user('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    except:
        pass
    assert 'Hello '+str(user)+'!' in retVal

def test_register_experiment_admin():
    user = 'awong'
    pwd = 'P@ssword10'
    email = 'awong@futurama.com'
    first_name = 'Amy'
    last_name = 'Wong'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    try:
        retVal = web_sess.register_user('Exp Admin',user,pwd,email,first_name,last_name,address,phone_number)
    except:
        pass
    assert 'Hello '+str(user)+'!' in retVal
