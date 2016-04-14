from WebUI.WebUI import WebUI
from os import path
from selenium.common.exceptions import NoSuchElementException
import pytest

web_sess = WebUI()


def test_patient_only_sees_his_data(logoff):
    """

    :param login_tpatient:
    :return:
    """
    print("Starting")

    patient = 'testPatient'
    patientpass = 'P@ssword10'
    patient2 = 'testPatient2'
    patient2pass = 'P@ssword10'
    file = path.abspath('./Data/BasisPeak/bodymetrics_simple.csv')
    fyear = '2015'
    fmonth = 'June'
    fday = '23'
    tyear = '2015'
    tmonth = 'June'
    tday = '23'
    device = 'basis'
    activities = []

    print("Upload data for ", patient2)
    web_sess.login(patient2, patient2pass)
    web_sess.upload_files(file, fyear, fmonth, fday, tyear, tmonth, tday, device, activities)
    web_sess.logoff()
    print("Login as ", patient)
    web_sess.login(patient, patientpass)
    print("Click View Data button")
    web_sess.driver.find_element_by_css_selector('View Data').click()
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
    bday = 'April 1, 1987'
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
    web_sess.create_patient(physician, phys_pass, patient, pat_pass, bday, loc, wght, hght, gen, race, eth)
    print('Logging in as', test_physician)
    web_sess.logoff()
    web_sess.login(test_physician, test_phys_pass)
    print('Checking for patient', patient)
    text = web_sess.driver.find_element_by_css_selector('div[class="card"]').get_attribute('innerHTML')
    assert '{} {}'.format(phys_fname, phys_lname) not in text

def test_experiment_creation_security(logoff):
    """
    **Requirements:**

    - 3.1.4.1: Experiments shall only be created by experiment admins

    **Pre Conditions:**

    - logoff fixture

    **Input:**



    ====================================  ===================  =============
    Steps                                 Expected Result      Actual Result
    ====================================  ===================  =============
    empty
    ====================================  ===================  =============
    """
    print("Starting")

    patient = 'testPatient'
    patientpass = 'P@ssword10'
    physician = 'testPhysician'
    physicianpass = 'P@ssword10'
    admin = 'fitadmin'
    adminpass = 'Password1!'

    print("Login as ", patient)
    web_sess.login(patient, patientpass)
    print("Look for Create Experiment button")
    pytest.raises(NoSuchElementException, web_sess.driver.find_element_by_link_text('Create Experiment'))
    print('Login as ', physician)
    web_sess.logoff()
    web_sess.login(physician, physicianpass)
    print('Look for Create Experiment button')
    pytest.raises(NoSuchElementException, web_sess.driver.find_element_by_link_text('Create Experiment'))
    print('Login as ', admin)
    web_sess.logoff()
    web_sess.login(admin, adminpass)
    print('Look for Create Experiment button')
    pytest.raises(NoSuchElementException, web_sess.driver.find_element_by_link_text('Create Experiment'))

def test_system_admin_user_management(logoff):
    """
    **Requirements:**

    - 3.1.6.4: Only system admin can enable accounts
    - 3.1.6.5: Only system admin can delete accounts

    **Pre Conditions:**

    - logoff fixture

    **Input:**



    ====================================  ===================  =============
    Steps                                 Expected Result      Actual Result
    ====================================  ===================  =============
    empty
    ====================================  ===================  =============
    """
    patient = 'testPatient'
    patientpass = 'P@ssword10'
    physician = 'testPhysician'
    physicianpass = 'P@ssword10'
    expadmin = 'testExpAdmin'
    expadminpass = 'P@ssword10'

    print("Login as ", patient)
    web_sess.login(patient, patientpass)
    print("Look for Manage Users button")
    pytest.raises(NoSuchElementException, web_sess.driver.find_element_by_link_text('Manage Users'))
    print('Login as ', physician)
    web_sess.logoff()
    web_sess.login(physician, physicianpass)
    print('Look for Manage Users button')
    pytest.raises(NoSuchElementException, web_sess.driver.find_element_by_link_text('Manage Users'))
    print('Login as ', expadmin)
    web_sess.logoff()
    web_sess.login(expadmin, expadminpass)
    print('Look for Manage Users button')
    pytest.raises(NoSuchElementException, web_sess.driver.find_element_by_link_text('Manage Users'))