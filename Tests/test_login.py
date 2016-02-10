from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_login_physician(logoff):
    web_sess.login("testPhysician","P@ssword10")
    status = web_sess.check_login()
    assert status, 'Could not login: \n{}'.format(web_sess.get_page())


def test_login_experiment_admin(logoff):
    web_sess.login("testExpAdmin",'P@ssword10')
    status = web_sess.check_login()
    assert status, 'Could not login: \n{}'.format(web_sess.get_page())


def test_login_patient(logoff):
    web_sess.login("testPatient",'P@ssword10')
    status = web_sess.check_login()
    assert status, 'Could not login: \n{}'.format(web_sess.get_page())


def test_login_bad_pass(logoff):
    web_sess.login('testPhysician','!nc0rrect')
    assert "Invalid login attempt." in web_sess.get_page()


def test_login_bad_user(logoff):
    web_sess.login('fake@uah.com','P@ssword10')
    assert "Invalid login attempt." in web_sess.get_page()
