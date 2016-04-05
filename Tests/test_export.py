"""
These test cases are designed to test the ability to export data from the system
"""
from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_export_experiment_results(logoff):
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

    assert False


def test_export_experiment_results_graphs(logoff):
    """
    **Requirements:**

    - 3.1.5: The system shall allow data exporting.
    - 3.1.5.1.1: The system will export experiment result graphs.

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

    assert False


def test_export_data_by_patient(logoff):
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

    assert False


def test_export_data_by_physician(logoff):
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

    assert False
