from WebUI.WebUI import WebUI

web_sess = WebUI()


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
