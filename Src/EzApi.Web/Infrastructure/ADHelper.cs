using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Configuration;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;
using System.Collections.Specialized;
using System.Collections;
using System.Diagnostics;
/*
* THANK YOU 
* http://www.c-sharpcorner.com/uploadfile/dhananjaycoder/all-operations-on-active-directory-ad-using-C-Sharp/
* http://www.jigsolving.com/activedirectory/user-account-attributes-part-1
* http://www.morgantechspace.com/2013/05/ldap-search-filter-examples.html
* 
* */
namespace Ez.Web.Infrastructure
{
    public enum eADUserLookupResult
    {
        NOT_SEARCHED = int.MinValue,
        USER_FOUND = 0,
        USER_NOT_FOUND = -1,
        USER_FOUND_WARNING = 1
    }
    public class ADUserLookupResult
    {
        public ADUserDetail User = null;
        public string Message = null;
        public eADUserLookupResult ReturnCode = eADUserLookupResult.NOT_SEARCHED;
    }

    public class ADUserLookupListResult
    {
        public List<ADUserDetail> UserList = new List<ADUserDetail>();
        public string Message = null;
        public eADUserLookupResult ReturnCode = eADUserLookupResult.NOT_SEARCHED;
        public ADUserLookupListResult Add(ADUserLookupResult res)
        {
            this.Message = res.Message;
            this.ReturnCode = res.ReturnCode;
            this.UserList.Add(res.User);
            return this;
        }
        public ADUserLookupListResult Add(string Message, eADUserLookupResult ReturnCode)
        {
            this.Message = Message;
            this.ReturnCode = ReturnCode;
            return this;
        }
        public ADUserLookupListResult Add(ADUserDetail res, string Message, eADUserLookupResult ReturnCode)
        {
            this.Message = Message;
            this.ReturnCode = ReturnCode;
            this.UserList.Add(res);
            return this;
        }
        public ADUserLookupListResult Add(ADUserDetail res)
        {
            this.UserList.Add(res);
            return this;
        }
        public ADUserLookupListResult Add(List<ADUserDetail> resList, string Message, eADUserLookupResult ReturnCode)
        {
            this.Message = Message;
            this.ReturnCode = ReturnCode;
            this.UserList.AddRange(resList);
            return this;
        }
        public ADUserLookupListResult Add(List<ADUserDetail> resList)
        {
            this.UserList.AddRange(resList);
            return this;
        }
    }

    public class ADUserDetail
    {
        private String _firstName;
        private String _middleName;
        private String _lastName;
        private String _loginName;
        private String _loginNameWithDomain;
        private String _streetAddress;
        private String _city;
        private String _state;
        private String _postalCode;
        private String _country;
        private String _homePhone;
        private String _extension;
        private String _mobile;
        private String _fax;
        private String _emailAddress;
        private String _title;
        private String _company;
        private String _manager;
        private String _managerName;
        private String _department;

        public String Department
        {
            get { return _department; }
        }

        public String FirstName
        {
            get { return _firstName; }
        }

        public String MiddleName
        {
            get { return _middleName; }
        }

        public String LastName
        {
            get { return _lastName; }
        }

        public String LoginName
        {
            get { return _loginName; }
        }

        public String LoginNameWithDomain
        {
            get { return _loginNameWithDomain; }
        }

        public String StreetAddress
        {
            get { return _streetAddress; }
        }

        public String City
        {
            get { return _city; }
        }

        public String State
        {
            get { return _state; }
        }

        public String PostalCode
        {
            get { return _postalCode; }
        }

        public String Country
        {
            get { return _country; }
        }

        public String HomePhone
        {
            get { return _homePhone; }
        }

        public String Extension
        {
            get { return _extension; }
        }

        public String Mobile
        {
            get { return _mobile; }
        }

        public String Fax
        {
            get { return _fax; }
        }

        public String EmailAddress
        {
            get { return _emailAddress; }
        }

        public String Title
        {
            get { return _title; }
        }

        public String Company
        {
            get { return _company; }
        }

        public ADUserDetail Manager
        {
            get
            {
                if (!String.IsNullOrEmpty(_managerName))
                {
                    ActiveDirectoryHelper ad = new ActiveDirectoryHelper();
                    return ad.GetUserByFullName(_managerName);
                }
                return null;
            }
        }

        public String ManagerName
        {
            get { return _managerName; }
        }


        public ADUserDetail()
        {

        }

        public ADUserDetail(string FirstName, string LastName, string LoginName)
        {
            _firstName = FirstName;
            _lastName = LastName;
            _loginName = LoginName;
        }
        public void CopyFrom(ADUserDetail ItemsToCopyFrom)
        {
            this._title = ItemsToCopyFrom.Title;
            this._streetAddress = ItemsToCopyFrom.StreetAddress;
            this._state = ItemsToCopyFrom.State;
            this._postalCode = ItemsToCopyFrom.PostalCode;
            this._mobile = ItemsToCopyFrom.Mobile;
            this._middleName = ItemsToCopyFrom.MiddleName;
            this._managerName = ItemsToCopyFrom.ManagerName;
            //this._manager = ItemsToCopyFrom.Manager.;
            this._loginNameWithDomain = ItemsToCopyFrom.LoginNameWithDomain;
            this._loginName = ItemsToCopyFrom.LoginName;
            this._lastName = ItemsToCopyFrom.LastName;
            this._homePhone = ItemsToCopyFrom.HomePhone;
            this._firstName = ItemsToCopyFrom.FirstName;
            this._fax = ItemsToCopyFrom.Fax;
            this._extension = ItemsToCopyFrom.Extension;
            this._emailAddress = ItemsToCopyFrom.EmailAddress;
            this._department = ItemsToCopyFrom.Department;
            this._country = ItemsToCopyFrom.Country;
            this._company = ItemsToCopyFrom.Company;
            this._city = ItemsToCopyFrom.City;
        }

        private ADUserDetail(DirectoryEntry directoryUser)
        {

            String domainAddress;
            String domainName;
            _firstName = GetProperty(directoryUser, ADProperties.FIRSTNAME);
            _middleName = GetProperty(directoryUser, ADProperties.MIDDLENAME);
            _lastName = GetProperty(directoryUser, ADProperties.LASTNAME);
            _loginName = GetProperty(directoryUser, ADProperties.LOGINNAME);
            String userPrincipalName = GetProperty(directoryUser, ADProperties.USERPRINCIPALNAME);
            if (!string.IsNullOrEmpty(userPrincipalName))
            {
                domainAddress = userPrincipalName.Split('@')[1];
            }
            else
            {
                domainAddress = String.Empty;
            }

            if (!string.IsNullOrEmpty(domainAddress))
            {
                domainName = domainAddress.Split('.').First();
            }
            else
            {
                domainName = String.Empty;
            }
            _loginNameWithDomain = String.Format(@"{0}\{1}", domainName, _loginName);
            _streetAddress = GetProperty(directoryUser, ADProperties.STREETADDRESS);
            _city = GetProperty(directoryUser, ADProperties.CITY);
            _state = GetProperty(directoryUser, ADProperties.STATE);
            _postalCode = GetProperty(directoryUser, ADProperties.POSTALCODE);
            _country = GetProperty(directoryUser, ADProperties.COUNTRY);
            _company = GetProperty(directoryUser, ADProperties.COMPANY);
            _department = GetProperty(directoryUser, ADProperties.DEPARTMENT);
            _homePhone = GetProperty(directoryUser, ADProperties.HOMEPHONE);
            _extension = GetProperty(directoryUser, ADProperties.EXTENSION);
            _mobile = GetProperty(directoryUser, ADProperties.MOBILE);
            _fax = GetProperty(directoryUser, ADProperties.FAX);
            _emailAddress = GetProperty(directoryUser, ADProperties.EMAILADDRESS);
            _title = GetProperty(directoryUser, ADProperties.TITLE);
            _manager = GetProperty(directoryUser, ADProperties.MANAGER);
            if (!String.IsNullOrEmpty(_manager))
            {
                String[] managerArray = _manager.Split(',');
                _managerName = managerArray[0].Replace("CN=", "");
            }
        }


        private static String GetProperty(DirectoryEntry userDetail, String propertyName)
        {
            if (userDetail.Properties.Contains(propertyName))
            {
                return userDetail.Properties[propertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static ADUserDetail GetUser(DirectoryEntry directoryUser)
        {
            return new ADUserDetail(directoryUser);
        }
    }

    public static class ADProperties
    {
        public const String OBJECTCLASS = "objectClass";
        public const String CONTAINERNAME = "cn";
        public const String LASTNAME = "sn";
        public const String COUNTRYNOTATION = "c";
        public const String CITY = "l";
        public const String STATE = "st";
        public const String TITLE = "title";
        public const String POSTALCODE = "postalCode";
        public const String PHYSICALDELIVERYOFFICENAME = "physicalDeliveryOfficeName";
        public const String FIRSTNAME = "givenName";
        public const String MIDDLENAME = "initials";
        public const String DISTINGUISHEDNAME = "distinguishedName";
        public const String INSTANCETYPE = "instanceType";
        public const String WHENCREATED = "whenCreated";
        public const String WHENCHANGED = "whenChanged";
        public const String DISPLAYNAME = "displayName";
        public const String USNCREATED = "uSNCreated";
        public const String MEMBEROF = "memberOf";
        public const String USNCHANGED = "uSNChanged";
        public const String COUNTRY = "co";
        public const String DEPARTMENT = "department";
        public const String COMPANY = "company";
        public const String PROXYADDRESSES = "proxyAddresses";
        public const String STREETADDRESS = "streetAddress";
        public const String DIRECTREPORTS = "directReports";
        public const String NAME = "name";
        public const String OBJECTGUID = "objectGUID";
        public const String USERACCOUNTCONTROL = "userAccountControl";
        public const String BADPWDCOUNT = "badPwdCount";
        public const String CODEPAGE = "codePage";
        public const String COUNTRYCODE = "countryCode";
        public const String BADPASSWORDTIME = "badPasswordTime";
        public const String LASTLOGOFF = "lastLogoff";
        public const String LASTLOGON = "lastLogon";
        public const String PWDLASTSET = "pwdLastSet";
        public const String PRIMARYGROUPID = "primaryGroupID";
        public const String OBJECTSID = "objectSid";
        public const String ADMINCOUNT = "adminCount";
        public const String ACCOUNTEXPIRES = "accountExpires";
        public const String LOGONCOUNT = "logonCount";
        public const String LOGINNAME = "sAMAccountName";
        public const String SAMACCOUNTTYPE = "sAMAccountType";
        public const String SHOWINADDRESSBOOK = "showInAddressBook";
        public const String LEGACYEXCHANGEDN = "legacyExchangeDN";
        public const String USERPRINCIPALNAME = "userPrincipalName";
        public const String EXTENSION = "ipPhone";
        public const String SERVICEPRINCIPALNAME = "servicePrincipalName";
        public const String OBJECTCATEGORY = "objectCategory";
        public const String DSCOREPROPAGATIONDATA = "dSCorePropagationData";
        public const String LASTLOGONTIMESTAMP = "lastLogonTimestamp";
        public const String EMAILADDRESS = "mail";
        public const String MANAGER = "manager";
        public const String MOBILE = "mobile";
        public const String PAGER = "pager";
        public const String FAX = "facsimileTelephoneNumber";
        public const String HOMEPHONE = "homePhone";
        public const String MSEXCHUSERACCOUNTCONTROL = "msExchUserAccountControl";
        public const String MDBUSEDEFAULTS = "mDBUseDefaults";
        public const String MSEXCHMAILBOXSECURITYDESCRIPTOR = "msExchMailboxSecurityDescriptor";
        public const String HOMEMDB = "homeMDB";
        public const String MSEXCHPOLICIESINCLUDED = "msExchPoliciesIncluded";
        public const String HOMEMTA = "homeMTA";
        public const String MSEXCHRECIPIENTTYPEDETAILS = "msExchRecipientTypeDetails";
        public const String MAILNICKNAME = "mailNickname";
        public const String MSEXCHHOMESERVERNAME = "msExchHomeServerName";
        public const String MSEXCHVERSION = "msExchVersion";
        public const String MSEXCHRECIPIENTDISPLAYTYPE = "msExchRecipientDisplayType";
        public const String MSEXCHMAILBOXGUID = "msExchMailboxGuid";
        public const String NTSECURITYDESCRIPTOR = "nTSecurityDescriptor";
    }
    public static class DomainManager
    {
        static DomainManager()
        {
            Domain domain = null;
            DomainController domainController = null;
            try
            {
                domain = Domain.GetCurrentDomain();
                DomainName = domain.Name;
                domainController = domain.PdcRoleOwner;
                DomainControllerName = domainController.Name.Split('.')[0];
                ComputerName = Environment.MachineName;
            }
            finally
            {
                if (domain != null)
                    domain.Dispose();
                if (domainController != null)
                    domainController.Dispose();
            }
        }

        public static string DomainControllerName { get; private set; }

        public static string ComputerName { get; private set; }

        public static string DomainName { get; private set; }

        public static string DomainPath
        {
            get
            {
                bool bFirst = true;
                StringBuilder sbReturn = new StringBuilder(200);
                string[] strlstDc = DomainName.Split('.');
                foreach (string strDc in strlstDc)
                {
                    if (bFirst)
                    {
                        sbReturn.Append("DC=");
                        bFirst = false;
                    }
                    else
                        sbReturn.Append(",DC=");

                    sbReturn.Append(strDc);
                }
                return sbReturn.ToString();
            }
        }

        public static string RootPath
        {
            get
            {
                return string.Format("LDAP://{0}/{1}", DomainName, DomainPath);
            }
        }
    }


    public class ActiveDirectoryHelper
    {
        public MemoryCacher mem = new MemoryCacher();
        public ADUserLookupResult ComplexActiveDirectoryLookup(string StringWithNameSomewhere)
        {
            return ComplexActiveDirectoryLookup(StringWithNameSomewhere, "");
        }

        public ADUserLookupResult ComplexActiveDirectoryLookup(string StringWithNameSomewhere, string PhoneNumber)
        {
            ADUserLookupResult res = new ADUserLookupResult();
            res.ReturnCode = eADUserLookupResult.USER_NOT_FOUND;
            string TestWhereUserWasFound = "";

            //Lets remote various words that we know won't help the search at all
            string[] arrNameParts = StringWithNameSomewhere.Replace("TESTING", "").Replace("USAF", "").Replace("AAFES", "").Replace("ARMY", "").Replace("AIRFORCE", "").Replace("EXCHANG", "").Replace("EXCHANGE", "").Replace("AAFES -", "").Trim().Split(' ');
            string MiddleInit = "";
            string sNamePart = "";
            if ((arrNameParts.Length == 1) && (arrNameParts[0].Length == 0))
            {
                TestWhereUserWasFound = "Not enough Name Info to search :(";
                res.Message = TestWhereUserWasFound;
            }
            else
            {
                //Lets look for the user id as the first word of the string block
                sNamePart = arrNameParts[0];
                TestWhereUserWasFound = "AD Lookup: UID as First Word (" + sNamePart + ")";
                var users = GetUserByLoginName(sNamePart);
                //var user1 = context.Users.FirstOrDefault(u => u.UserName == sNamePart);
                if ((users != null) && (users.LoginName != null)) return new ADUserLookupResult() { Message = TestWhereUserWasFound, User = users, ReturnCode = eADUserLookupResult.USER_FOUND };

                //Lets do a user name lookup on every part of the string and see if we get a match,  since it appears the middle name will usually be at the end
                //  if we enounter that first, then lets assume that is the middle initial
                if (arrNameParts[arrNameParts.Length - 1].Length == 1)
                {
                    MiddleInit = arrNameParts[arrNameParts.Length - 1];
                    arrNameParts = arrNameParts.Where(w => w != arrNameParts[arrNameParts.Length - 1]).ToArray();
                }
                //if we didn't find a middle intial,  lets see if there is a middle inital with a period
                if (MiddleInit.Length == 0)
                {
                    for (int i = arrNameParts.Length - 1; i >= 0; i--)
                    {
                        sNamePart = arrNameParts[i];
                        if (sNamePart.Length == 1)
                        {
                            MiddleInit = sNamePart;
                            arrNameParts = arrNameParts.Where(w => w != arrNameParts[i]).ToArray();
                        }
                        else if ((sNamePart.Length == 2) && (sNamePart.EndsWith(".")))
                        {
                            MiddleInit = sNamePart.Replace(".", "");
                            arrNameParts = arrNameParts.Where(w => w != arrNameParts[i]).ToArray();
                        }
                    }
                }
                //  We only have 2 words,  so lets assume format is First and Last Name or ( First Middle Last ) if we plucked it out of the array previously
                if (arrNameParts.Length == 2)
                {
                    if (MiddleInit.Length > 0)
                    {
                        TestWhereUserWasFound = "AD Lookup: F MI L format (" + arrNameParts[0] + " " + MiddleInit + " " + arrNameParts[1] + ")";
                        //var user2A = context.Users.irstOrDefault(u => ((u.FirstName == arrNameParts[0]) && (u.MiddleInitial == MiddleInit) && (u.LastName == arrNameParts[1])));
                        var user2A = GetUsersByName(arrNameParts[0], MiddleInit, arrNameParts[1]);
                        return new ADUserLookupResult() { Message = TestWhereUserWasFound, User = user2A.First(), ReturnCode = eADUserLookupResult.USER_FOUND };
                    }
                    else
                    {
                        TestWhereUserWasFound = "AD Lookup: F L format (" + arrNameParts[0] + " " + arrNameParts[1] + ")";
                        var Users = GetUsersByName(arrNameParts[0], arrNameParts[1]);
                        if (Users.Count > 0)
                        {
                            if (Users.Count == 1)
                            {
                                return new ADUserLookupResult() { Message = TestWhereUserWasFound, User = Users.First(), ReturnCode = eADUserLookupResult.USER_FOUND };
                            }
                            else
                            {
                                return new ADUserLookupResult() { Message = TestWhereUserWasFound + ".  Warning: Multiple Entries Found!", User = Users.First(), ReturnCode = eADUserLookupResult.USER_FOUND_WARNING };
                            }
                        }
                    }
                }
            }
            if (PhoneNumber.Length > 0)
            {
                //Lets try a phone number search, first since it is essentially free text, lets seperate it into common formats
                List<string> phoneFormats = new List<string>();
                double dPhoneNumber = double.Parse(PhoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", ""));
                phoneFormats.Add(String.Format("{0:###-###-####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:##########}", dPhoneNumber));
                /*phoneFormats.Add(String.Format("{0:(###) ###-####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:1-###-###-####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:+1-###-###-####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:### #######}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:### ### ####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:###-### ####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:### ###-####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:######-####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:###-#######}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:(###) #######}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:(###)#######}", dPhoneNumber));*/
                foreach (string phoneNum in phoneFormats)
                {
                    ADUserDetail firstFound = null;
                    TestWhereUserWasFound = "AD Lookup: Mobile or Main Phone Search (" + phoneNum + ")";
                    var Users = GetUsersByPhone(phoneNum);
                    foreach (ADUserDetail usr in Users)
                    {
                        if ((firstFound == null) && ((arrNameParts.Contains(usr.FirstName, StringComparer.OrdinalIgnoreCase)) || (arrNameParts.Contains(usr.LastName, StringComparer.OrdinalIgnoreCase))))
                        {
                            firstFound = usr;
                        }
                    }
                    if (Users.Count > 0)
                    {
                        if (firstFound != null)
                        {
                            return new ADUserLookupResult() { Message = TestWhereUserWasFound + " and partial name match" + ((Users.Count == 1) ? "" : ".  Warning: Multiple Entries Found!"), User = firstFound, ReturnCode = eADUserLookupResult.USER_FOUND };
                        }
                        else
                        {
                            return new ADUserLookupResult() { Message = TestWhereUserWasFound + ((Users.Count == 1) ? "" : ".  Warning: Multiple Entries Found!"), User = Users.First(), ReturnCode = eADUserLookupResult.USER_FOUND };
                        }
                    }
                }
            }
            if (arrNameParts.Length == 1)
            {
            }

            return new ADUserLookupResult() { Message = "Not found in AD", ReturnCode = eADUserLookupResult.USER_NOT_FOUND };
        }

        public ADUserLookupListResult ComplexActiveDirectoryLookupList(string StringWithNameSomewhere, string PhoneNumber)
        {
            ADUserLookupListResult res = new ADUserLookupListResult();
            res.ReturnCode = eADUserLookupResult.USER_NOT_FOUND;
            string TestWhereUserWasFound = "";
            if (StringWithNameSomewhere.Contains(","))
            {
                string[] arrNamePartsComma = StringWithNameSomewhere.Split(',');
                if (arrNamePartsComma.Length == 2)
                {
                    StringWithNameSomewhere = (arrNamePartsComma[1] + ' ' + arrNamePartsComma[0]).Trim();
                }
            }

            //Lets remote various words that we know won't help the search at all
            string[] arrNameParts = StringWithNameSomewhere.Replace("TESTING", "").Replace("USAF", "").Replace("AAFES", "").Replace("ARMY", "").Replace("AIRFORCE", "").Replace("EXCHANG", "").Replace("EXCHANGE", "").Replace("AAFES -", "").Trim().Split(' ');
            string MiddleInit = "";
            string sNamePart = "";
            List<ADUserDetail> usersByLanId = new List<ADUserDetail>();
            List<ADUserDetail> usersByF_MI_L = new List<ADUserDetail>();
            List<ADUserDetail> usersByF_L = new List<ADUserDetail>();
            List<ADUserDetail> usersByPhone = new List<ADUserDetail>();
            List<ADUserDetail> usersByF = new List<ADUserDetail>();
            List<ADUserDetail> usersByL = new List<ADUserDetail>();
            List<ADUserDetail> usersFullByL = new List<ADUserDetail>();
            List<ADUserDetail> usersFullByF = new List<ADUserDetail>();
            if ((arrNameParts.Length == 1) && (arrNameParts[0].Length == 0))
            {
                TestWhereUserWasFound = "Not enough Name Info to search :(";
                res.Message = TestWhereUserWasFound;
            }
            else
            {
                //Lets look for the user id as the first word of the string block
                sNamePart = arrNameParts[0];
                TestWhereUserWasFound = "AD Lookup: UID as First Word (" + sNamePart + ")";
                var userByLanId = GetUserByLoginName(sNamePart);
                if ((userByLanId != null) && (userByLanId.LoginName != null)) usersByLanId.Add(userByLanId);
                //var user1 = context.Users.FirstOrDefault(u => u.UserName == sNamePart);
                //if ((usersByLanId != null) && (usersByLanId.LoginName != null)) return res.Add(users, TestWhereUserWasFound, eADUserLookupResult.USER_FOUND);

                //Lets do a user name lookup on every part of the string and see if we get a match,  since it appears the middle name will usually be at the end
                //  if we enounter that first, then lets assume that is the middle initial
                if (arrNameParts[arrNameParts.Length - 1].Length == 1)
                {
                    MiddleInit = arrNameParts[arrNameParts.Length - 1];
                    arrNameParts = arrNameParts.Where(w => w != arrNameParts[arrNameParts.Length - 1]).ToArray();
                }
                //if we didn't find a middle intial,  lets see if there is a middle inital with a period
                if (MiddleInit.Length == 0)
                {
                    for (int i = arrNameParts.Length - 1; i >= 0; i--)
                    {
                        sNamePart = arrNameParts[i];
                        if (sNamePart.Length == 1)
                        {
                            MiddleInit = sNamePart;
                            arrNameParts = arrNameParts.Where(w => w != arrNameParts[i]).ToArray();
                        }
                        else if ((sNamePart.Length == 2) && (sNamePart.EndsWith(".")))
                        {
                            MiddleInit = sNamePart.Replace(".", "");
                            arrNameParts = arrNameParts.Where(w => w != arrNameParts[i]).ToArray();
                        }
                    }
                }
                //  We only have 2 words,  so lets assume format is First and Last Name or ( First Middle Last ) if we plucked it out of the array previously
                if (arrNameParts.Length == 2)
                {
                    if (MiddleInit.Length > 0)
                    {
                        TestWhereUserWasFound = "AD Lookup: F MI L format (" + arrNameParts[0] + " " + MiddleInit + " " + arrNameParts[1] + ")";
                        //var user2A = context.Users.irstOrDefault(u => ((u.FirstName == arrNameParts[0]) && (u.MiddleInitial == MiddleInit) && (u.LastName == arrNameParts[1])));
                        var user2A = GetUsersByName(arrNameParts[0], MiddleInit, arrNameParts[1]);
                        usersByF_MI_L.AddRange(user2A);
                        //return res.Add( new ADUserLookupResult() { Message = TestWhereUserWasFound, User = user2A.First(), ReturnCode = eADUserLookupResult.USER_FOUND });
                    }
                    else
                    {
                        TestWhereUserWasFound = "AD Lookup: F L format (" + arrNameParts[0] + " " + arrNameParts[1] + ")";
                        var Users = GetUsersByName(arrNameParts[0], arrNameParts[1]);
                        if (Users.Count > 0)
                        {
                            usersByF_L.AddRange(Users);
                            /*if (Users.Count == 1)
                            {
                                return res.Add( Users.First(), TestWhereUserWasFound, eADUserLookupResult.USER_FOUND);
                            }
                            else
                            {
                                return res.Add(Users, TestWhereUserWasFound + ".  Warning: Multiple Entries Found!", eADUserLookupResult.USER_FOUND );
                                //return new ADUserLookupResult() { Message = TestWhereUserWasFound + ".  Warning: Multiple Entries Found!", User = Users.First(), ReturnCode = eADUserLookupResult.USER_FOUND_WARNING };
                            }*/
                        }
                    }
                }
            }
            if (PhoneNumber.Length > 0)
            {
                //Lets try a phone number search, first since it is essentially free text, lets seperate it into common formats
                List<string> phoneFormats = new List<string>();
                double dPhoneNumber = double.Parse(PhoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", ""));
                phoneFormats.Add(String.Format("{0:###-###-####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:##########}", dPhoneNumber));
                /*phoneFormats.Add(String.Format("{0:(###) ###-####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:1-###-###-####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:+1-###-###-####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:### #######}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:### ### ####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:###-### ####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:### ###-####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:######-####}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:###-#######}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:(###) #######}", dPhoneNumber));
                phoneFormats.Add(String.Format("{0:(###)#######}", dPhoneNumber));*/
                foreach (string phoneNum in phoneFormats)
                {
                    ADUserDetail firstFound = null;
                    TestWhereUserWasFound = "AD Lookup: Mobile or Main Phone Search (" + phoneNum + ")";
                    var Users = GetUsersByPhone(phoneNum);
                    foreach (ADUserDetail usr in Users)
                    {
                        if ((firstFound == null) && ((arrNameParts.Contains(usr.FirstName, StringComparer.OrdinalIgnoreCase)) || (arrNameParts.Contains(usr.LastName, StringComparer.OrdinalIgnoreCase))))
                        {
                            firstFound = usr;
                        }
                    }
                    if (Users.Count > 0)
                    {
                        usersByPhone.AddRange(Users);
                        /*
                        if (firstFound != null)
                        {
                            return res.Add(Users, TestWhereUserWasFound + " and partial name match" + ((Users.Count == 1) ? "" : ".  Warning: Multiple Entries Found!"), eADUserLookupResult.USER_FOUND);
                        }
                        else
                        {
                            return res.Add(Users, TestWhereUserWasFound + ((Users.Count == 1) ? "" : ".  Warning: Multiple Entries Found!"), eADUserLookupResult.USER_FOUND);
                        }
                         * */
                    }
                }
            }
            if (arrNameParts.Length == 1)
            {
                TestWhereUserWasFound = "AD Lookup: Using " + arrNameParts[0] + " as F (or) L";
                var Users = GetUsersByName(arrNameParts[0]);
                if (Users.Count > 0)
                {
                    usersByF.AddRange(Users);
                    //return res.Add(Users, TestWhereUserWasFound + ((Users.Count == 1) ? "" : ".  Warning: Multiple Entries Found!"), eADUserLookupResult.USER_FOUND);
                }
                //TestWhereUserWasFound = "AD Lookup: Using " + arrNameParts[0] + " as L";
                var Users2 = GetUsersByName("", "", arrNameParts[0]);
                if (Users2.Count > 0)
                {
                    usersByL.AddRange(Users2);
                    //return res.Add(Users2, TestWhereUserWasFound + ((Users2.Count == 1) ? "" : ".  Warning: Multiple Entries Found!"), eADUserLookupResult.USER_FOUND);
                }
            }
            var usrlistL = GetUsersByName("", "", StringWithNameSomewhere);
            if (usrlistL.Count > 0)
            {
                usersFullByL.AddRange(usrlistL);
            }
            var usrlistF = GetUsersByName(StringWithNameSomewhere, "", "");
            if (usrlistF.Count > 0)
            {
                usersFullByF.AddRange(usrlistF);
            }

            var UsersJoin = usersByLanId.Union(usersByF_MI_L).Union(usersByF_L).Union(usersByPhone).Union(usersByF).Union(usersByL).Union(usersFullByL).Union(usersFullByF).Distinct().OrderBy(x => x.LoginName).ToList();
            if (UsersJoin.Count > 0)
            {
                //UsersJoin.
                return res.Add(UsersJoin, TestWhereUserWasFound + ((UsersJoin.Count == 1) ? "" : ".  Warning: Multiple Entries Found!"), eADUserLookupResult.USER_FOUND);
            }

            return res.Add("Not found in AD", eADUserLookupResult.USER_NOT_FOUND);
            //return new ADUserLookupResult() { Message = "Not found in AD", ReturnCode = ADUserLookupResult.USER_NOT_FOUND };
        }
        private DirectoryEntry _directoryEntry = null;

        private DirectoryEntry SearchRoot
        {
            get
            {
                if (_directoryEntry == null)
                {
                    _directoryEntry = new DirectoryEntry(DomainManager.RootPath);
                }
                return _directoryEntry;
            }
        }

        internal ADUserDetail GetUserByFullName(String userName)
        {
            try
            {
                _directoryEntry = null;
                DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
                directorySearch.Filter = "(&(objectClass=user)(cn=" + userName + "))";
                SearchResult results = directorySearch.FindOne();

                if (results != null)
                {
                    DirectoryEntry user = new DirectoryEntry(results.Path);
                    return ADUserDetail.GetUser(user);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return null;
            }
        }

        public ADUserDetail GetUserByLoginName(String userName)
        {
            ADUserDetail ret = null;
            try
            {
                if ((userName == null) || (userName.Length == 0))
                {
                    ret = new ADUserDetail("", "", "");
                    return ret;
                }
                if (userName.Equals("ITDBSYS"))
                {
                    ret = new ADUserDetail("ITCheckbook", "System", "ITDBSYS");
                }
                else if (userName != null)
                {
                    _directoryEntry = null;
                    DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
                    directorySearch.Filter = "(&(objectClass=user)(SAMAccountName=" + userName + "))";
                    SearchResult results = directorySearch.FindOne();

                    if (results != null)
                    {
                        DirectoryEntry user = new DirectoryEntry(results.Path);
                        ret = ADUserDetail.GetUser(user);
                    }
                }
                if (ret == null)
                {
                    ret = new ADUserDetail();  //create blank user record
                    //If we did not find the active directory record, cache it for a much shorter time in anticipation for its addition
                    mem.Upsert("ADLOOKUP_" + userName, ret, DateTimeOffset.Now.AddMinutes(10));
                }
                else
                {

                    //If we did find the AD record, cache the result for 24 hours
                    mem.Upsert("ADLOOKUP_" + userName, ret, DateTimeOffset.Now.AddHours(24));
                }
                return ret;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<string> GetUserGroupMembership(string strUser)
        {
            List<string> groups = new List<string>();
            try
            {
                DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
                directorySearch.Filter = "(&(objectClass=user)(SAMAccountName=" + strUser + "))";
                SearchResult res = directorySearch.FindOne();
                if (null != res)
                {
                    DirectoryEntry obUser = new DirectoryEntry(res.Path);
                    // Invoke Groups method.
                    object obGroups = obUser.Invoke("Groups");
                    foreach (object ob in (IEnumerable)obGroups)
                    {
                        // Create object for each group.
                        DirectoryEntry obGpEntry = new DirectoryEntry(ob);
                        groups.Add(obGpEntry.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw;
            }
            return groups;
        }
        /// <summary>
        /// This function will take a DL or Group name and return list of users
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public List<ADUserDetail> GetUserFromGroup(String groupName)
        {
            List<ADUserDetail> userlist = new List<ADUserDetail>();
            try
            {
                _directoryEntry = null;
                DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
                directorySearch.Filter = "(&(objectClass=group)(SAMAccountName=" + groupName + "))";
                SearchResult results = directorySearch.FindOne();
                if (results != null)
                {

                    DirectoryEntry deGroup = new DirectoryEntry(results.Path);
                    System.DirectoryServices.PropertyCollection pColl = deGroup.Properties;
                    int count = pColl["member"].Count;


                    for (int i = 0; i < count; i++)
                    {
                        string respath = results.Path;
                        string[] pathnavigate = respath.Split("CN".ToCharArray());
                        respath = pathnavigate[0];
                        string objpath = pColl["member"][i].ToString();
                        string path = respath + objpath;


                        DirectoryEntry user = new DirectoryEntry(path);
                        ADUserDetail userobj = ADUserDetail.GetUser(user);
                        userlist.Add(userobj);
                        user.Close();
                    }
                }
                return userlist;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return userlist;
            }

        }

        #region Get user with First Name

        public List<ADUserDetail> GetUsersByFirstName(string fName)
        {

            //UserProfile user;
            List<ADUserDetail> userlist = new List<ADUserDetail>();
            string filter = "";

            _directoryEntry = null;
            DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
            directorySearch.Asynchronous = true;
            directorySearch.CacheResults = true;
            filter = string.Format("(givenName={0}*", fName);
            //            filter = "(&(objectClass=user)(objectCategory=person)(givenName="+fName+ "*))";
            //(&(objectCategory=person)(objectClass=user)(!sAMAccountType=805306370)(sn=Morgan))

            directorySearch.Filter = filter;

            SearchResultCollection userCollection = directorySearch.FindAll();
            foreach (SearchResult users in userCollection)
            {
                DirectoryEntry userEntry = new DirectoryEntry(users.Path);
                ADUserDetail userInfo = ADUserDetail.GetUser(userEntry);

                userlist.Add(userInfo);

            }

            directorySearch.Filter = "(&(objectClass=group)(SAMAccountName=" + fName + "*))";
            SearchResultCollection results = directorySearch.FindAll();
            if (results != null)
            {

                foreach (SearchResult r in results)
                {
                    DirectoryEntry deGroup = new DirectoryEntry(r.Path);

                    ADUserDetail agroup = ADUserDetail.GetUser(deGroup);
                    userlist.Add(agroup);
                }

            }
            return userlist;
        }

        #endregion

        #region Get user with First And Last Name

        public List<ADUserDetail> GetUsersByName(string fName)
        {
            return GetUsersByName(fName, "", "");
        }
        public List<ADUserDetail> GetUsersByName(string fName, string lName)
        {
            return GetUsersByName(fName, "", lName);
        }
        public List<ADUserDetail> GetUsersByName(string fName, string mInitial, string lName)
        {

            //UserProfile user;
            List<ADUserDetail> userlist = new List<ADUserDetail>();
            string filter = "";

            _directoryEntry = null;
            DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
            directorySearch.Asynchronous = true;
            directorySearch.CacheResults = true;
            if ((fName.Length > 0) && (mInitial.Length == 0) && (lName.Length == 0))
            {
                filter = string.Format("(&(objectCategory=person)(objectClass=user)(givenName={0}))", fName);
            }
            else if ((fName.Length > 0) && (mInitial.Length == 0) && (lName.Length > 0))
            {
                filter = string.Format("(&(objectCategory=person)(objectClass=user)(givenName={0})(sn={1}))", fName, lName);
            }
            else if ((fName.Length > 0) && (mInitial.Length > 0) && (lName.Length > 0))
            {
                filter = string.Format("(&(objectCategory=person)(objectClass=user)(givenName={0})(initials={1})(sn={2}))", fName, mInitial, lName);
            }
            else if ((fName.Length == 0) && (mInitial.Length == 0) && (lName.Length > 0))
            {
                filter = string.Format("(&(objectCategory=person)(objectClass=user)(sn={0}))", lName);
            }
            else
            {
                throw new Exception("Invalid Parameters passed!!");
            }
            //            filter = "(&(objectClass=user)(objectCategory=person)(givenName="+fName+ "*))";
            //(&(objectCategory=person)(objectClass=user)(!sAMAccountType=805306370)(sn=Morgan))

            directorySearch.Filter = filter;

            SearchResultCollection userCollection = directorySearch.FindAll();
            foreach (SearchResult users in userCollection)
            {
                DirectoryEntry userEntry = new DirectoryEntry(users.Path);
                ADUserDetail userInfo = ADUserDetail.GetUser(userEntry);

                userlist.Add(userInfo);

            }
            return userlist;
        }

        #endregion


        #region Get user with Phone Number

        public List<ADUserDetail> GetUsersByPhone(string PhoneNumber)
        {

            //UserProfile user;
            List<ADUserDetail> userlist = new List<ADUserDetail>();
            Dictionary<string, ADUserDetail> userlistIndexed = new Dictionary<string, ADUserDetail>();
            string filter = "";

            _directoryEntry = null;
            DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
            directorySearch.Asynchronous = true;
            directorySearch.CacheResults = true;
            filter = string.Format("(&(objectCategory=person)(objectClass=user)(|(telephoneNumber={0})(mobile={0})))", PhoneNumber, PhoneNumber);

            directorySearch.Filter = filter;

            SearchResultCollection userCollection = directorySearch.FindAll();
            foreach (SearchResult users in userCollection)
            {
                DirectoryEntry userEntry = new DirectoryEntry(users.Path);
                ADUserDetail userInfo = ADUserDetail.GetUser(userEntry);
                userlistIndexed.Add(userInfo.LoginName, userInfo);

            }
            return userlistIndexed.Values.ToList();
        }

        #endregion

        #region AddUserToGroup
        public bool AddUserToGroup(string userlogin, string groupName)
        {
            try
            {
                _directoryEntry = null;
                ADManager admanager = new ADManager();
                admanager.AddUserToGroup(userlogin, groupName);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }
        #endregion

        #region RemoveUserToGroup
        public bool RemoveUserToGroup(string userlogin, string groupName)
        {
            try
            {
                _directoryEntry = null;
                ADManager admanager = new ADManager();
                admanager.RemoveUserFromGroup(userlogin, groupName);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }
        #endregion
    }

    public class ADManager
    {

        PrincipalContext context;

        public ADManager()
        {
            context = new PrincipalContext(ContextType.Machine);

        }


        public ADManager(string domain, string container)
        {
            context = new PrincipalContext(ContextType.Domain, domain, container);
        }

        public ADManager(string domain, string username, string password)
        {
            context = new PrincipalContext(ContextType.Domain, username, password);
        }

        public bool AddUserToGroup(string userName, string groupName)
        {
            bool done = false;
            GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupName);
            if (group == null)
            {
                group = new GroupPrincipal(context, groupName);
            }
            UserPrincipal user = UserPrincipal.FindByIdentity(context, userName);
            if (user != null & group != null)
            {
                group.Members.Add(user);
                group.Save();
                done = (user.IsMemberOf(group));
            }
            return done;
        }


        public bool RemoveUserFromGroup(string userName, string groupName)
        {
            bool done = false;
            UserPrincipal user = UserPrincipal.FindByIdentity(context, userName);
            GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupName);
            if (user != null & group != null)
            {
                group.Members.Remove(user);
                group.Save();
                done = !(user.IsMemberOf(group));
            }
            return done;
        }
    }
}