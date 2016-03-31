"""
These test cases are designed to test the ability to create and delete accounts
"""
from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_create_physician(logoff):
    """
    **Requirements:**

    - 3.1.1.1.2.1: minimum information stored for physician account
    - 3.1.1.1.4.2: System Administrators shall be able to enable users
    - 3.1.1.7: Physicians create their own accounts
    - 3.1.1.7.1: System Administrators shall verify the creation of a physician account
    - 3.1.6.1: The system shall allow account creation
    - 3.1.6.1.1: The system shall force account approval for physicians


    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'hfarnswroth'
    - pwd = 'P@ssword10'
    - email = 'hfarnsworth@futurama.com'
    - first_name = 'Hubert'
    - last_name = 'Farnsworth'
    - address = '304 Wherever Street, New New York City, New New York'
    - phone_number = '123-456-7890'


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    request account  Request confirmed
    approve account  Admin can approve
    login            User can login
    ===============  =================  =============
    """
    print('Starting')

    user = 'hfarnswroth'
    pwd = 'P@ssword10'
    email = 'hfarnsworth@futurama.com'
    first_name = 'Hubert'
    last_name = 'Farnsworth'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    print('Requesting Exp Admin {}/{} name: {}, {} email: {} address: {} phone num: {}'.format(user,
                                                                                               pwd,
                                                                                               last_name,
                                                                                               first_name,
                                                                                               email,
                                                                                               address,
                                                                                               phone_number))
    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    assert web_sess.check_request_account()
    print('Approving account for {}, {}'.format(last_name, first_name))
    web_sess.approve_account('{} {}'.format(first_name, last_name))
    print('Logging in as {}/{}'.format(user, pwd))
    web_sess.login(user, pwd)
    assert web_sess.check_login()


def test_create_experiment_admin(logoff):
    """
    **Requirements:**

    - 3.1.1.1.3.1: minimum information stored for experiment admin account
    - 3.1.1.1.4.2: System Administrators shall be able to enable users
    - 3.1.1.7: Experiment administrators create their own accounts
    - 3.1.1.7.1: System Administrators shall verify the creation of a experiment admin account
    - 3.1.6.1: The system shall allow account creation
    - 3.1.6.1.1: The system shall force account approval for experiment admins


    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'awong'
    - pwd = 'P@ssword10'
    - email = 'awong@futurama.com'
    - first_name = 'Amy'
    - last_name = 'Wong'
    - address = '304 Wherever Street, New New York City, New New York'
    - phone_number = '123-456-7890'


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    request account  Request confirmed
    approve account  Admin can approve
    login            User can login
    ===============  =================  =============
    """
    print('Starting')

    user = 'awong'
    pwd = 'P@ssword10'
    email = 'awong@futurama.com'
    first_name = 'Amy'
    last_name = 'Wong'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    print('Requesting Exp Admin {}/{} name: {}, {} email: {} address: {} phone num: {}'.format(user,
                                                                                               pwd,
                                                                                               last_name,
                                                                                               first_name,
                                                                                               email,
                                                                                               address,
                                                                                               phone_number))
    web_sess.request_account('Exp Admin',user,pwd,email,first_name,last_name,address,phone_number)
    assert web_sess.check_request_account()
    print('Approving account for {}, {}'.format(last_name, first_name))
    web_sess.approve_account('{} {}'.format(first_name, last_name))
    print('Logging in as {}/{}'.format(user, pwd))
    web_sess.login(user, pwd)
    assert web_sess.check_login()


def test_create_patient(login_tphysician):
    """
    **Requirements:**

    - 3.1.1.1.1.1: minimum information stored for patient account
    - 3.1.1.1.2.5: Physician user can add a patient to the system
    - 3.1.1.3.1: Display patient ID to physician at the time of creation
    - 3.1.1.7.2: Physicians shall create their patients accounts
    - 3.1.6.1: The system shall allow account creation
    - 3.1.6.1.2: Patients shall not need approval for account creation

    **Pre Conditions:**

    - login_tphysician fixture

    **Input:**

    - user = 'pfry'
    - pwd = 'P@ssword10'
    - bday = '3 March, 1954'
    - loc = 'Alabama'
    - wght = '200'
    - hght = '72'
    - gen = 'male'
    - race = 'white'
    - eth = 'non_hispanic'
    - physician = 'testPhysician'

    ======================  =================  =============
    Steps                   Expected Result    Actual Result
    ======================  =================  =============
    create patient account  No error msgs
    login as patient        Patient can login
    ======================  =================  =============
    """
    print('Starting')

    user = 'pfry'
    pwd = 'P@ssword10'
    bday = '3 March, 1954'
    loc = 'Alabama'
    wght = '200'
    hght = '72'
    gen = 'male'
    race = 'white'
    eth = 'non_hispanic'
    physician = 'testPhysician'

    print('Physician {} is creating patient account.'.format(physician),
          'user: ', user,
          'pwd: ', pwd,
          'bday: ', bday,
          'loc: ', loc,
          'wght: ', wght,
          'hght: ', hght,
          'gen: ', gen,
          'race: ', race,
          'eth: ', eth)
    web_sess.create_patient(physician,
                        pwd,
                        user,
                        pwd,
                        bday,
                        loc,
                        wght,
                        hght,
                        gen,
                        race,
                        eth)
    assert web_sess.check_create_patient()
    print('Logging in as ', user, '/', pwd)
    web_sess.login(user, pwd)
    assert web_sess.check_login()


def test_delete_patient(logoff):
    """
    **Requirements:**

    - 3.1.1.1.2.6: Physicians can delete patients

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'pfry'
    - pwd = 'P@ssword10'
    - phy = 'testPhysician'
    - phy_pass = 'P@ssword10'

    ======================  ====================  =============
    Steps                   Expected Result       Actual Result
    ======================  ====================  =============
    delete patient account  No error msgs
    login as patient        Patient cannot login
    ======================  ====================  =============
    """
    print('Starting')

    user = 'pfry'
    pwd = 'P@ssword10'
    phy = 'testPhysician'
    phy_pass = 'P@ssword10'

    print('Deleting patient', user, 'from physician ', phy)
    web_sess.delete_patient(phy, phy_pass, user)
    print('Trying to login as ', user)
    web_sess.login(user, pwd)
    assert not web_sess.check_login()


def test_delete_physician(logoff):
    """
    **Requirements:**

    - 3.1.6.3: System administrator can delete accounts

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'hfarnswroth'
    - username = 'Hubert Farnsworth'
    - pwd = 'P@ssword10'

    ========================  ====================  =============
    Steps                     Expected Result       Actual Result
    ========================  ====================  =============
    delete physician account  No error msgs
    login as patient          User cannot login
    ========================  ====================  =============
    """
    print('Starting')

    user = 'hfarnswroth'
    username = 'Hubert Farnsworth'
    pwd = 'P@ssword10'

    print('Deleting user with name ', username)
    web_sess.delete_account(username)
    print('Attempting to login as ', user)
    web_sess.login(user, pwd)
    assert not web_sess.check_login()


def test_delete_experiment_admin(logoff):
    """
    **Requirements:**

    - 3.1.6.3: System administrator can delete accounts

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'awong'
    - username = 'Amy Wong'
    - pwd = 'P@ssword10'

    ===============================  ====================  =============
    Steps                            Expected Result       Actual Result
    ===============================  ====================  =============
    delete experiment admin account  No error msgs
    login as patient                 User cannot login
    ===============================  ====================  =============
    """
    print('Starting')

    user = 'awong'
    username = 'Amy Wong'
    pwd = 'P@ssword10'

    print('Deleting user with name ', username)
    web_sess.delete_account(username)
    print('Attempting to login as ', user)
    web_sess.login(user, pwd)
    assert not web_sess.check_login()
