from WebUI.WebUI import WebUI

web_sess = WebUI()

def test_request_physician():
    user = 'hfarnswroth'
    pwd = 'P@ssword10'
    email = 'hfarnsworth@futurama.com'
    first_name = 'Hubert'
    last_name = 'Farnsworth'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'
    retVal = ''

    try:
        retVal = web_sess.request_account('Physician',user,pwd,email,first_name,last_name,address,phone_number)
    except:
        pass
    assert web_sess.check_request_account()

def test_request_experiment_admin():
    user = 'awong'
    pwd = 'P@ssword10'
    email = 'awong@futurama.com'
    first_name = 'Amy'
    last_name = 'Wong'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'
    retVal = ''

    try:
        retVal = web_sess.request_account('Exp Admin',user,pwd,email,first_name,last_name,address,phone_number)
    except:
        pass
    assert web_sess.check_request_account()
