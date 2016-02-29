"""
These tests are to verify that the accounts can be logged into
"""
from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_login_physician(logoff):
    """
    This test will login as the testPhysician.
    Validation is on the login step.

    :param logoff: Makes sure the session is in a logged off state at the start of the test
    :return:
    """
    web_sess.login('testPhysician', 'P@ssword10')
    status = web_sess.check_login()
    assert status, 'Could not login: \n{}'.format(web_sess.get_page())


def test_login_experiment_admin(logoff):
    """
    This test will login as the testExpAdmin.
    Validation is on the login step.

    :param logoff: Makes sure the session is in a logged off state at the start of the test
    :return:
    """
    web_sess.login('testExpAdmin', 'P@ssword10')
    status = web_sess.check_login()
    assert status, 'Could not login: \n{}'.format(web_sess.get_page())


def test_login_patient(logoff):
    """
    This test will login as the testPatient.
    Validation is on the login step.

    :param logoff: Makes sure the session is in a logged off state at the start of the test
    :return:
    """
    web_sess.login('testPatient', 'P@ssword10')
    status = web_sess.check_login()
    assert status, 'Could not login: \n{}'.format(web_sess.get_page())


def test_login_system_admin(logoff):
    """
    This test will login as the fitadmin.
    Validation is on the login step.

    :param logoff: Makes sure the session is in a logged off state at the start of the test
    :return:
    """
    web_sess.login('fitadmin', 'Password1!')
    status = web_sess.check_login()
    assert status, 'Could not login: \n{}'.format(web_sess.get_page())


def test_login_bad_pass(logoff):
    """
    This test will verify that a user cannot login with an incorrect password
    Validation is done by checking for "Invalid login attempt" in the web page

    :param logoff: Makes sure the session is in a logged off state at the start of the test
    :return:
    """
    web_sess.login('testPhysician', '!nc0rrect')
    assert "Invalid login attempt." in web_sess.get_page()


def test_login_bad_user(logoff):
    """
    This test will verify that a user cannot login with an incorrect username
    Validation is done by checking for "Invalid login attempt" in the web page

    :param logoff: Makes sure the session is in a logged off state at the start of the test
    :return:
    """
    web_sess.login('fake', 'P@ssword10')
    assert "Invalid login attempt." in web_sess.get_page()
