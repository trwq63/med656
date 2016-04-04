"""
These tests are to verify that the accounts can be logged into
"""
from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_login_physician(logoff):
    """
    **Requirements:**

    - 3.1.1: System shall provide user authentication
    - 3.1.1.5: System shall allow user to log out of account

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'testPhysician'
    - password = 'P@ssword10'

    **Expected Results:**

    User 'testPhysician' sees the home page after logging in

    ========  ===============  =============
    Steps     Expected Result  Actual Result
    ========  ===============  =============
    login     Login Success!
    ========  ===============  =============
    """
    print('Starting')
    user = 'testPhysician'
    password = 'P@ssword10'

    print('Logging in as {}/{}'.format(user, password))
    web_sess.login(user, password)
    print('Checking for Manage button')
    status = web_sess.check_login()
    assert status


def test_login_experiment_admin(logoff):
    """
    **Requirements:**

    - 3.1.1: System shall provide user authentication
    - 3.1.1.5: System shall allow user to log out of account

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'testExpAdmin'
    - password = 'P@ssword10'

    **Expected Results:**

    User 'testPhysician' sees the home page after logging in

    ========  ===============  =============
    Steps     Expected Result  Actual Result
    ========  ===============  =============
    login     Login Success!
    ========  ===============  =============
    """
    print('Starting')

    user = 'testExpAdmin'
    password = 'P@ssword10'

    print('Logging in as {}/{}'.format(user, password))
    web_sess.login(user, password)
    print('Checking for Manage button')
    status = web_sess.check_login()
    assert status


def test_login_patient(logoff):
    """
    **Requirements:**

    - 3.1.1: System shall provide user authentication
    - 3.1.1.5: System shall allow user to log out of account

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'testPatient'
    - password = 'P@ssword10'

    **Expected Results:**

    User 'testPhysician' sees the home page after logging in

    ========  ===============  =============
    Steps     Expected Result  Actual Result
    ========  ===============  =============
    login     Login Success!
    ========  ===============  =============
    """
    print('Starting')

    user = 'testPatient'
    password = 'P@ssword10'

    print('Logging in as {}/{}'.format(user, password))
    web_sess.login(user, password)
    print('Checking for Manage button')
    status = web_sess.check_login()
    assert status


def test_login_system_admin(logoff):
    """
    **Requirements:**

    - 3.1.1: System shall provide user authentication
    - 3.1.1.5: System shall allow user to log out of account

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'fitadmin'
    - password = 'Password1!'

    **Expected Results:**

    User 'testPhysician' sees the home page after logging in

    ========  ===============  =============
    Steps     Expected Result  Actual Result
    ========  ===============  =============
    login     Login Success!
    ========  ===============  =============
    """
    print('Starting')

    user = 'fitadmin'
    password = 'Password1!'

    print('Logging in as {}/{}'.format(user, password))
    web_sess.login(user, password)
    status = web_sess.check_login()
    assert status


def test_login_bad_pass(logoff):
    """
    **Requirements:**

    - 3.1.1: System shall provide user authentication

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'testPhysician'
    - password = '!nc0rrect'
    - expected = 'Invalid login attempt.'

    **Expected Results:**

    User 'testPhysician' sees the home page after logging in

    ========  ===============  =============
    Steps     Expected Result  Actual Result
    ========  ===============  =============
    login     Invalid login
    ========  ===============  =============
    """
    print('Starting')

    user = 'testPhysician'
    password = '!nc0rrect'
    expected = 'Invalid login attempt.'

    print('Logging in as {}/{}'.format(user, password))
    web_sess.login(user, password)
    print('Checking for {} in web page'.format(expected))
    assert expected in web_sess.get_page()


def test_login_bad_user(logoff):
    """
    **Requirements:**

    - 3.1.1: System shall provide user authentication

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'fake'
    - password = 'P@ssword10'
    - expected = 'Invalid login attempt.'

    **Expected Results:**

    User 'testPhysician' sees the home page after logging in

    ========  ===============  =============
    Steps     Expected Result  Actual Result
    ========  ===============  =============
    login     Invalid login
    ========  ===============  =============
    """
    print('Starting')

    user = 'fake'
    password = '!nc0rrect'
    expected = 'Invalid login attempt.'

    print('Logging in as {}/{}'.format(user, password))
    web_sess.login(user, password)
    assert expected in web_sess.get_page()
