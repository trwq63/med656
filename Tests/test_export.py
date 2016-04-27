"""
These test cases are designed to test the ability to export data from the system
"""
from WebUI.WebUI import WebUI
from time import sleep
import random

web_sess = WebUI()


def test_export_experiment_results(login_texpadmin):
    """
    **Requirements:**

    - 3.1.5: The system shall allow data exporting.
    - 3.1.5.1: The system shall allow experiment results to be exported.
    - 3.1.5.4: The system shall provide comma-separated values (.csv) file exports.

    **Pre Conditions:**

    - login_texpadmin fixture

    **Input:**

    - exp_name = 'test_experiment'
    - sage = '18'
    - eage = '90'
    - shght = '60'
    - ehght = '80'
    - swght = '100'
    - ewght = '250'
    - genders = []
    - races = []
    - eth = []
    - loc = []

    ============================================  ====================================  =============
    Steps                                         Expected Result                       Actual Result
    ============================================  ====================================  =============
    create experiemnt                             no errors
    view the experiment                           there are patients in the experiment
    view the data of a patient in the experiment  no errors
    click the export button                       no errors
    manually check for the file                   files have been downloaded
    ============================================  ====================================  =============
    """
    print('Starting')

    exp_name = 'test_experiment'
    sage = '18'
    eage = '90'
    shght = '60'
    ehght = '80'
    swght = '100'
    ewght = '250'
    genders = []
    races = []
    eth = []
    loc = []

    #download_path = "C:/tmp/{}".format(random.getrandbits(100))

    print('Creating an experiment: ', exp_name)
    assert web_sess.create_experiment(exp_name, sage, eage, shght, ehght, swght, ewght, genders, races, eth, loc)
    print('Viewing experiment: ', exp_name)
    assert web_sess.view_experiment(exp_name)
    print('View the data of a patient in the experiment')
    view_buttons = web_sess.driver.find_elements_by_css_selector('input[value="View Data"]')

    p = 0
    while p < len(view_buttons):
        button = view_buttons[p]
        button.click()
        print('Try to export data from patient ', p)
        try:
            exp_button = web_sess.driver.find_element_by_xpath('//button[text()="Export"]')
        except Exception:
            print('No data found')
            web_sess.driver.back()
            web_sess.driver.refresh()
            view_buttons = web_sess.driver.find_elements_by_css_selector('input[value="View Data"]')
            p += 1
            continue
        #web_sess.driver.find_element_by_id('directory').send_keys(download_path)
        exp_button.click()
        break

    #print('Check for file here: ', download_path)
    print('check the download folder for the file')


def test_export_data_by_patient(login_tpatient, test_patients):
    """
    **Requirements:**

    - 3.1.5: The system shall allow data exporting.
    - 3.1.5.2: The system shall allow patients to export their data.
    - 3.1.5.4: The system shall provide comma-separated values (.csv) file exports.

    **Pre Conditions:**

    - login_tpatient fixture

    **Input:**


    =================================  ============================  =============
    Steps                              Expected Result               Actual Result
    =================================  ============================  =============
    go to export page                  your username is visible
    click on your user                 you can see an uploaded file
    click on the first available file  file gets highlighted
    fill in the download path          no errors
    click on the export button         download starts
    manually check for the file        files exist on the system
    =================================  ============================  =============

    **NOTE:**

    This test does not work with the latest export changes that were made. It can be run manually for verification.
    """
    print('Starting')

    #download_path = "C:/tmp/{}".format(random.getrandbits(100))

    print('Go to export page')
    sleep(5)
    web_sess.driver.find_element_by_link_text('Export Data').click()
    print('Click on your user')
    web_sess.driver.find_element_by_xpath('//td[text()="{}"]'.format(test_patients[0]['name'])).click()
    print('Click on the first available file')
    sleep(2)
    web_sess.driver.find_element_by_css_selector('label[class="verticalSelect__label"]').click()
    #print('Fill in download path')
    #web_sess.driver.find_element_by_id('directory').send_keys(download_path)
    print('Click on the export button')
    web_sess.driver.find_element_by_id('btnExport').click()
    #print('Check for file here: ', download_path)
    print('Check the download folder for the file')


def test_export_data_by_physician(login_tphysician, test_patients):
    """
    **Requirements:**

    - 3.1.5: The system shall allow data exporting.
    - 3.1.5.3: The system shall allow physicians to export the data of their patients.
    - 3.1.5.4: The system shall provide comma-separated values (.csv) file exports.

    **Pre Conditions:**

    - login_tphysician fixture

    **Input:**


    ==========================  =======================  =============
    Steps                       Expected Result          Actual Result
    ==========================  =======================  =============
    click on the export button  you see the patient
    click on the patient        you see files listed
    click on the first file     the file is highlighted
    fill in the download path   no errors
    click the export button     the download starts
    check for the file          the file exises
    ==========================  =======================  =============

    **NOTE:**

    This test does not work with the latest export changes that were made. It can be run manually for verification.
    """
    print('Starting')

    #download_path = "C:/tmp/{}".format(random.getrandbits(100))

    print('Click on the export button')
    sleep(5)
    web_sess.driver.find_element_by_link_text('Export Data').click()
    print('Click on the test patient')
    web_sess.driver.find_element_by_xpath('//td[text()="{}"]'.format(test_patients[0]['name'])).click()
    print('Click on the first file')
    sleep(5)
    web_sess.driver.find_element_by_class_name('verticalSelect__label').click()
    #print('Fill in download path')
    #web_sess.driver.find_element_by_id('driver').send_keys(download_path)
    print('Click on export')
    web_sess.driver.find_element_by_id('btnExport')
    #print('Check for file here: ', download_path)
    print('Check the download folder for the file')


