from WebUI.WebUI import WebUI

web_sess = WebUI()

def test_login_physician():
    retVal = web_sess.login("testPhysician","P@ssword10")
    if "Invalid login attempt." not in retVal:
        web_sess.logoff()
    assert web_sess.check_login()

def test_login_experiment_admin():
    retVal = web_sess.login("testExpAdmin",'P@ssword10')
    if "Invalid login attempt." not in retVal:
        web_sess.logoff()
    assert web_sess.check_login()

def test_login_bad_pass():
    retVal = web_sess.login('testPhysician','!nc0rrect')
    assert "Invalid login attempt." in retVal

def test_login_bad_user():
    retVal = web_sess.login('fake@uah.com','P@ssword10')
    assert "Invalid login attempt." in retVal
