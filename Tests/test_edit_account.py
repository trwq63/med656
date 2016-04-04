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


    ========================  =================  =============
    Steps                     Expected Result    Actual Result
    ========================  =================  =============
    Go to management page     No Errors
    Change the email address  No Errors
    Check the email address   Email has changed
    ========================  =================  =============
    """
    assert web_sess.set_account_info(email='tphysician@yahoo.com')
    assert web_sess.confirm_account_info(email='tphysician@yahoo.com')

