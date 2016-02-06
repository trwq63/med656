from WebUI.WebUI import WebUI

web_sess = WebUI()

def test_login_physician():
    retVal = web_sess.login("testPhysician","P@ssword10")
    web_sess.logoff()
    assert "Invalid login attempt." not in retVal

def test_login_experiment_admin():
    retVal = web_sess.login("testExpAdmin",'P@ssword10')
    web_sess.logoff()
    assert "Invalid login attempt." not in retVal

def test_login_bad_pass():
    #web_sess = WebUI(pwd='!nc0rrect')
    try:
        retVal = web_sess.login(pwd='!nc0rrect')
    except:
        pass
    assert "Invalid login attempt" in retVal

def test_login_bad_user():
    #web_sess = WebUI(user='fake@uah.com')
    try:
        retVal = web_sess.login(user='fake@uah.com')
    except:
        pass
    assert "Invalid login attempt." in retVal
