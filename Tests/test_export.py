"""
These test cases are designed to test the ability to export data from the system
"""
from WebUI.WebUI import WebUI
import os

web_sess = WebUI()


def test_export_experiment_results(login_texpadmin):
    """
    **Requirements:**

    - 3.1.5: The system shall allow data exporting.
    - 3.1.5.1: The system shall allow experiment results to be exported.
    - 3.1.5.4: The system shall provide comma-separated values (.csv) file exports.

    **Pre Conditions:**

    - logoff fixture

    **Input:**


    =================  =================  =============
    Steps              Expected Result    Actual Result
    =================  =================  =============
    create experiemnt
    =================  =================  =============
    """
    print('Starting')

    exp_name = 'test_experiment'
    sage = '18'
    eage = '70'
    shght = '60'
    ehght = '80'
    swght = '100'
    ewght = '250'
    genders = ['male']
    races = []
    eth = []
    loc = []

    print('Creating an experiment: ', exp_name)
    assert web_sess.create_experiment(exp_name, sage, eage, shght, ehght, swght, ewght, genders, races, eth, loc)
    print('Viewing experiment: ', exp_name)
    assert web_sess.view_experiment(exp_name)
    print('View the data of a patient in the experiment')
    assert web_sess.view_data()
    print('Export all the data')
    assert web_sess.export_data()
    print('Check for file')
    assert os.path.isfile('file')


def test_export_data_by_patient(login_tpatient, test_patients):
    """
    **Requirements:**

    - 3.1.5: The system shall allow data exporting.
    - 3.1.5.2: The system shall allow patients to export their data.
    - 3.1.5.4: The system shall provide comma-separated values (.csv) file exports.

    **Pre Conditions:**

    - logoff fixture

    **Input:**


    =================  =================  =============
    Steps              Expected Result    Actual Result
    =================  =================  =============
    create experiemnt
    =================  =================  =============
    """
    print('Starting')

    print('Go to export page')
    web_sess.driver.find_element_by_link_text('Export Data').click()
    print('Click on your user')
    web_sess.driver.find_element_by_xpath('//td[text()="{}"]'.format(test_patients[0]['name']))
    print('Click on the first available file')
    web_sess.driver.find_element_by_css_selector('label[class="verticalSelect__label"]').click()
    print('Click on the export button')
    web_sess.driver.find_element_by_id('btnExport').click()
    print('Check download directory for file')
    assert os.path.isfile('file')


def test_export_data_by_physician(login_tphysician, test_patients):
    """
    **Requirements:**

    - 3.1.5: The system shall allow data exporting.
    - 3.1.5.3: The system shall allow physicians to export the data of their patients.
    - 3.1.5.4: The system shall provide comma-separated values (.csv) file exports.

    **Pre Conditions:**

    - logoff fixture

    **Input:**


    =================  =================  =============
    Steps              Expected Result    Actual Result
    =================  =================  =============
    create experiemnt
    =================  =================  =============
    """
    print('Starting')

    print('Click on the export button')
    web_sess.driver.find_element_by_link_text('Export Data').click()
    print('Click on the test patient')
    web_sess.driver.find_element_by_xpath('//td[text()="{}"]'.format(test_patients[0]['name']))
    print('Click on the first file')
    web_sess.driver.find_element_by_class_name('verticalSelect__label').click()
    print('Click on export')
    web_sess.driver.find_element_by_id('btnExport')
    print('Check download directory for file')
    assert os.path.isfile('file')

