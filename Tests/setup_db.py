from shutil import copy


def main():
    copy('./Data/database/aspnet-UAHFitVault-20160110104509.mdf',
         '../Source/UAHFitVault/UAHFitVault/App_Data/aspnet-UAHFitVault-20160110104509.mdf')
    copy('./Data/database/aspnet-UAHFitVault-20160110104509_log.ldf',
         '../Source/UAHFitVault/UAHFitVault/App_Data/aspnet-UAHFitVault-20160110104509_log.ldf')

if __name__ == '__main__':
    main()
