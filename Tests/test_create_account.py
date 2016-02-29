"""
These test cases are designed to test the ability to create and delete accounts
"""
from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_create_physician(logoff):
    """
    This test will request an Physician account of hfarnsworth, approve the account and login as this user
    Validation is done at the request account and login steps.

    :param logoff: Makes sure the web session is in a logged off state at the start of the test
    :return:
    """
    user = 'hfarnswroth'
    pwd = 'P@ssword10'
    email = 'hfarnsworth@futurama.com'
    first_name = 'Hubert'
    last_name = 'Farnsworth'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    assert web_sess.check_request_account()
    web_sess.approve_account(user)
    web_sess.login(user, pwd)
    assert web_sess.check_login()


def test_create_experiment_admin(logoff):
    """
    This test will request an Exp Admin account of awong, approve the account and login as this user
    Validation is done at the request account and login steps.

    :param logoff: Makes sure the web session is in a logged off state at the start of the test
    :return:
    """
    user = 'awong'
    pwd = 'P@ssword10'
    email = 'awong@futurama.com'
    first_name = 'Amy'
    last_name = 'Wong'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    web_sess.request_account('Exp Admin',user,pwd,email,first_name,last_name,address,phone_number)
    assert web_sess.check_request_account()
    web_sess.approve_account(user)
    web_sess.login(user, pwd)
    assert web_sess.check_login()


def test_create_patient(login_tphysician):
    """
    This test will create a patient account of pfry from the tphysician login and login as pfry
    Validation is done on the create patient step and login step

    :param login_tphysician: Makes sure the web session is logged in as tphysician at the start of the test
    :return:
    """
    user = 'pfry'
    pwd = 'P@ssword10'
    email = 'pfry@futurama.com'
    first_name = 'Phillip'
    last_name = 'Fry'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    web_sess.create_patient(user,pwd,email,first_name,last_name,address,phone_number)
    # note: this needs to be updated to check the patient creation
    assert web_sess.check_request_account()
    web_sess.login(user, pwd)
    assert web_sess.check_login()


def test_delete_patient(logoff):
    """
    This test will delete a patient account using the System Admin account and verify the user cannot login
    Validation is done on the login step
    Note: this is incorrect. It needs to verify that Physicians can delete patient accounts rq# 3.1.1.1.2.6

    :param logoff: Makes sure the web session is in a logged off state at the start of the test
    :return:
    """
    user = 'pfry'
    pwd = 'P@ssword10'

    web_sess.delete_account(user)
    web_sess.login(user, pwd)
    assert web_sess.check_login()


def test_delete_physician(logoff):
    """
    This test will delete a physician account using the System Admin account and verify the user cannot login
    Validation is done on the login step

    :param logoff: Makes sure the web session is in a logged off state at the start of the test
    :return:
    """
    user = 'hfarnswroth'
    pwd = 'P@ssword10'

    web_sess.delete_account(user)
    web_sess.login(user, pwd)
    assert web_sess.check_login()


def test_delete_experiment_admin(logoff):
    """
    This test will delete an experiment admin account using the System Admin account and verify the user cannot login
    Validation is done on the login step

    :param logoff: Makes sure the web session is in a logged off state at the start of the test
    :return:
    """
    user = 'awong'
    pwd = 'P@ssword10'

    web_sess.delete_account(user)
    web_sess.login(user, pwd)
    assert web_sess.check_login()
