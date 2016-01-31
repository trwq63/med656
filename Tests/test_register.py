from WebUI.WebUI import WebUI

web_sess = WebUI()
user = 'test3@uah.edu'

def test_register():
    try:
        retVal = web_sess.register_user(user=user)
    except:
        pass
    assert 'Hello '+str(user)+'!' in retVal
