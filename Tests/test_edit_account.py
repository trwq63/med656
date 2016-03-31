"""
These tests are here to verify that the accounts can have their information edited.
"""
from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_physician_can_edit_his_account(login_tphysician):
    """
    **Requirements:**

    - 3.1.1.1.2.2: Physician can update his account
    - 3.1.6.2: System shall allow users to edit their account

    **Pre Conditions:**

    - login_tphysician fixture

    **Input:**


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============

    ===============  =================  =============
    """
    assert web_sess.set_account_info(email='tphysician@yahoo.com')
    assert web_sess.confirm_account_info(email='tphysician@yahoo.com')


def test_sys_admin_can_edit_patient_account(login_sysadmin):
    """
    **Requirements:**

    - 3.1.1.1.4.4: System administrator can update patient account information

    **Pre Conditions:**

    - login_sysadmin fixture

    **Input:**


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============

    ===============  =================  =============
    """
    assert False


def test_sys_admin_can_edit_exp_admin_account(login_sysadmin):
    """
    **Requirements:**

    - 3.1.1.1.4.4: System administrator can update patient account information

    **Pre Conditions:**

    - login_sysadmin fixture

    **Input:**


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============

    ===============  =================  =============
    """
    assert False


def test_sys_admin_can_edit_physician_account(login_sysadmin):
    """
    **Requirements:**

    - 3.1.1.1.4.4: System administrator can update patient account information

    **Pre Conditions:**

    - login_sysadmin fixture

    **Input:**


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============

    ===============  =================  =============
    """
    assert False
