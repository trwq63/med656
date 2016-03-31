from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_patient_to_patient(logoff):
    """
    **Requirements:**

    - 3.1.1.1.1.3: Patient cannot view other patients data

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'testPatient'
    - pwd = 'P@ssword10'


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    login            User can login
    ===============  =================  =============
    """
    print('Starting')

    user = 'testPatient'
    pwd = 'P@ssword10'

    print('Logging in as ', user)
    web_sess.login(user, pwd)
    ps = web_sess.driver.get('')
    assert 'Restricted' in ps


def test_physician_to_patient(logoff):
    """
    **Requirements:**

    - 3.1.1.1.2.4: Physician can only view his patients data

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = ''
    - pwd = ''

    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    login            User can login
    ===============  =================  =============
    """
    print('Starting')

    user = ''
    pwd = ''

    print('Logging in as ', user)
    web_sess.login(user, pwd)
    ps = web_sess.driver.get('')
    assert 'Restricted' in ps


def test_patient_cannot_create_patient(logoff):
    """
    **Requirements:**

    - 3.1.1.1.2.5: Only a physician can create a patient

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = ''
    - pwd = ''


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    login            User can login
    ===============  =================  =============
    """
    print('Starting')

    user = ''
    pwd = ''

    print('Logging in as ', user)
    web_sess.login(user, pwd)
    assert False


def test_exp_admin_cannot_create_patient():
    """
    **Requirements:**

    - 3.1.1.1.2.5: Only a physician can create a patient

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = ''
    - pwd = ''


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    login            User can login
    ===============  =================  =============
    """
    print('Starting')

    user = ''
    pwd = ''

    print('Logging in as ', user)
    web_sess.login(user, pwd)
    assert False
