from WebUI.WebUI import WebUI
from os import path
from selenium.common.exceptions import NoSuchElementException
import pytest

web_sess = WebUI()


def test_patient_only_sees_his_data(test_patients):
    """
    **Requirements:**

    - 3.1.1.1.2.4: Physician can only view his patients data

    **Pre Conditions:**

    - test_patients fixture

    **Input:**

    - file = path.abspath('./Data/BasisPeak/bodymetrics_2012-01-30T00-00-00_2016-01-18T04-51-00.csv')
    - fyear = '2015'
    - fmonth = 'June'
    - fday = '23'
    - tyear = '2015'
    - tmonth = 'June'
    - tday = '23'
    - device = 'basis'
    - activities = []

    ========================================  ==============================================  =============
    Steps                                     Expected Result                                 Actual Result
    ========================================  ==============================================  =============
    login as the test patient 2               your at the home page
    upload a test data file                   no errors
    logoff                                    back at the login screen
    login as test patient 1                   your at the home page
    click the view data button                you see all the files that have been uploaded
    look for the file that was just uploaded  you do not see the file you uploaded in step 2
    ========================================  ==============================================  =============
    """
    print("Starting")

    file = path.abspath('./Data/BasisPeak/bodymetrics_2012-01-30T00-00-00_2016-01-18T04-51-00.csv')
    fyear = '2015'
    fmonth = 'June'
    fday = '23'
    tyear = '2015'
    tmonth = 'June'
    tday = '23'
    device = 'basis'
    activities = []

    web_sess.logoff()
    print("Upload data for ", test_patients[1]['name'])
    web_sess.login(test_patients[1]['name'], test_patients[1]['pwd'])
    web_sess.upload_files(file, fyear, fmonth, fday, tyear, tmonth, tday, device, activities)
    web_sess.logoff()
    print("Login as ", test_patients[0]['name'])
    web_sess.login(test_patients[0]['name'], test_patients[0]['pwd'])
    print("Click View Data button")
    web_sess.driver.find_element_by_link_text('View Data').click()
    print("Look for file ", path.basename(file))
    # TO DO: update this to not use get_page()
    assert path.basename(file) not in web_sess.get_page()


def test_physician_to_patient(logoff):
    """
    **Requirements:**

    - 3.1.1.1.2.4: Physician can only view his patients data

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - physician = 'testPhysician3'
    - phys_pass = 'P@ssword10'
    - phys_email = 'testphysician3@uah.com'
    - phys_add = 'here'
    - phys_fname = 'test'
    - phys_lname = 'physicianthree'
    - phys_phone = '123-456-7890'
    - patient = 'testPatient5'
    - pat_pass = 'P@ssword10'
    - bday = 'April 1, 1987'
    - loc = 'Alabama'
    - wght = '123'
    - hght = '123'
    - gen = 'male'
    - race = 'white'
    - eth = 'non_hispanic'
    - test_physician = 'testPhysician'
    - test_phys_pass = 'P@ssword10'

    ====================================  ===================  =============
    Steps                                 Expected Result      Actual Result
    ====================================  ===================  =============
    create physician                      no errors
    create patient for physician          no errors
    login as testPhysician                login success
    check for patient in management page  patient not listed
    ====================================  ===================  =============
    """
    print('Starting')

    physician = 'testPhysician3'
    phys_pass = 'P@ssword10'
    phys_email = 'testphysician3@uah.com'
    phys_add = 'here'
    phys_fname = 'test'
    phys_lname = 'physicianthree'
    phys_phone = '123-456-7890'
    patient = 'testPatient5'
    pat_pass = 'P@ssword10'
    byear = '1987'
    bmonth = 'April'
    bday = '1'
    loc = 'Alabama'
    wght = '123'
    hght = '123'
    gen = 'male'
    race = 'white'
    eth = 'non_hispanic'
    test_physician = 'testPhysician'
    test_phys_pass = 'P@ssword10'

    print('Creating physician ', physician)
    web_sess.request_account('Physician', physician, phys_pass, phys_email, phys_fname, phys_lname, phys_add, phys_phone)
    print('Apprpving physician', physician)
    web_sess.approve_account('{} {}'.format(phys_fname, phys_lname))
    print('Creating patient', patient, ' for physician ', physician)
    web_sess.logoff()
    web_sess.create_patient(physician, phys_pass, patient, pat_pass, byear, bmonth, bday, loc, wght, hght, gen, race, eth)
    print('Logging in as', test_physician)
    web_sess.logoff()
    web_sess.login(test_physician, test_phys_pass)
    print('Checking for patient', patient)
    text = web_sess.driver.find_element_by_css_selector('div[class="card"]').get_attribute('innerHTML')
    assert '{} {}'.format(phys_fname, phys_lname) not in text

def test_experiment_creation_security(login_tpatient):
    """
    **Requirements:**

    - 3.1.4.1: Experiments shall only be created by experiment admins

    **Pre Conditions:**

    - login_tpatient fixture

    **Input:**

    - physician = 'testPhysician'
    - physicianpass = 'P@ssword10'
    - admin = 'fitadmin'
    - adminpass = 'Password1!'

    =====================================  ====================================  =============
    Steps                                  Expected Result                       Actual Result
    =====================================  ====================================  =============
    look for the create experiment button  there is no create experiment button
    logoff and login as testPhysician      no errors
    look for the create experiment button  there is no create experiment button
    logoff and login as fitadmin           no errors
    look for the create experiment button  there is no create experiment button
    =====================================  ====================================  =============
    """
    print("Starting")

    physician = 'testPhysician'
    physicianpass = 'P@ssword10'
    admin = 'fitadmin'
    adminpass = 'Password1!'

    print("Look for Create Experiment button")
    try:
        web_sess.driver.find_element_by_link_text('Create Experiment')
        assert False
    except Exception as e:
        print('Could not find "Create Experiment" button')

    print('Login as ', physician)
    web_sess.logoff()
    web_sess.login(physician, physicianpass)
    print('Look for Create Experiment button')
    try:
        web_sess.driver.find_element_by_link_text('Create Experiment')
        assert False
    except Exception as e:
        print('Could not find "Create Experiment" button')

    print('Login as ', admin)
    web_sess.logoff()
    web_sess.login(admin, adminpass)
    print('Look for Create Experiment button')
    try:
        web_sess.driver.find_element_by_link_text('Create Experiment')
        assert False
    except Exception as e:
        print('Could not find "Create Experiment" button')

def test_system_admin_user_management(login_tpatient):
    """
    **Requirements:**

    - 3.1.6.4: Only system admin can enable accounts
    - 3.1.6.5: Only system admin can delete accounts

    **Pre Conditions:**

    - login_tpatient fixture

    **Input:**

    - physician = 'testPhysician'
    - physicianpass = 'P@ssword10'
    - expadmin = 'testExpAdmin'
    - expadminpass = 'P@ssword10'

    =====================================  ==================================  =============
    Steps                                  Expected Result                     Actual Result
    =====================================  ==================================  =============
    look for the manage users button       manage users button does not exist
    logoff and login as the testPhysician  no errors
    look for the manage users button       manage users button does not exist
    logoff and login as the testExpAdmin   no errors
    look for the manage users button       manage users button does not exist
    =====================================  ==================================  =============
    """
    print('Starting')

    physician = 'testPhysician'
    physicianpass = 'P@ssword10'
    expadmin = 'testExpAdmin'
    expadminpass = 'P@ssword10'

    print("Look for Manage Users button")
    try:
        web_sess.driver.find_element_by_link_text('Manage Users')
        assert False
    except Exception as e:
        print('Could not find "Manage Users" button')

    print('Login as ', physician)
    web_sess.logoff()
    web_sess.login(physician, physicianpass)
    print('Look for Manage Users button')
    try:
        web_sess.driver.find_element_by_link_text('Manage Users')
        assert False
    except Exception as e:
        print('Could not find "Manage Users" button')

    print('Login as ', expadmin)
    web_sess.logoff()
    web_sess.login(expadmin, expadminpass)
    print('Look for Manage Users button')
    try:
        web_sess.driver.find_element_by_link_text('Manage Users')
        assert False
    except Exception as e:
        print('Could not find "Manage Users" button')
