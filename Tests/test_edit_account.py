"""
These tests are here to verify that the accounts can have their information edited.
"""
from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_physician_can_edit_his_account(login_tphysician):
    """
    This test will edit the testPhysician's email address and verify the email address got changed
    Validation is on both the edit step and the confirmation step

    :param login_tphysician: Makes sure the session is logged in as the testPhysician at the start of the test
    :return:
    """
    assert web_sess.set_account_info(email='tphysician@yahoo.com')
    assert web_sess.confirm_account_info(email='tphysician@yahoo.com')


def test_sys_admin_can_edit_patient_account(login_sysadmin):
    assert False


def test_sys_admin_can_edit_exp_admin_account(login_sysadmin):
    assert False


def test_sys_admin_can_edit_physician_account(login_sysadmin):
    assert False
