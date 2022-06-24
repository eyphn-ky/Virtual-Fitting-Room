using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    //Meassure variables
    [Header("Meassure")]
    public TMP_InputField usernameField;
    public TMP_InputField sizeField;
    public TMP_InputField waistField;
    public TMP_InputField chestField;
    public TMP_InputField hipField;
    public TMP_Text meassureText;
    


    void Awake()
    {
        
        //Firebase için gerekli tüm bağımlılıkların sistemde mevcut olup olmadığını kontrol eder
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                
                //Bağlantı varsa Firebase'i başlat
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Firebase Bağlantı Hatası!: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase() //Start
    {
        Debug.Log("Setting up Firebase Auth");
        //Authentication set ediliyor 
        auth = FirebaseAuth.DefaultInstance;
        //Firebase Realtime Database bağlantısı sağlanıyor
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        
    }

    //Ekranlar arası geçişlerde dolu olan textler temizlenir.
    public void ClearLoginFields()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }

    public void ClearRegisterFields()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }

    //Giriş yapma butonu fonksiyonu
    public void LoginButton()
    {

        // E-mail ve şifre ile Kimlik doğrulama 
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Kayıt olma butonu fonksiyonu
    public void RegisterButton()
    {
        

        //E-postayı, şifreyi ve kullanıcı adını içeren kayıt olma ekranını çağıran buton
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    //Çıkış yapma butonu fonksiyonu
    public void signOutButton()
    {
        auth.SignOut();
        UIManager.instance.LoginScreen();
        ClearLoginFields();
        ClearRegisterFields();
    }
    public void SaveDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(usernameField.text));
        StartCoroutine(UpdateUsernameDatabase(usernameField.text));

        StartCoroutine(UpdateSize(int.Parse(sizeField.text)));
        StartCoroutine(UpdateWaist(int.Parse(waistField.text)));
        StartCoroutine(UpdateChest(int.Parse(chestField.text)));
        StartCoroutine(UpdateHip(int.Parse(hipField.text)));

        meassureText.text = "Ölçüleriniz Başarıyla Kaydedildi!";

        SceneManager.LoadScene("MainScene");
    }

    //Kullanıcılar için giriş yapma kontrolleri
    private IEnumerator Login(string _email, string _password)
    {
      
        //E-posta ve şifreyi ileten Firebase auth oturum açma işlevinin çağırılması. (Asenkron olarak Firebase database'deki kayıtlı kullanıvı kontrolü)
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        
        //Kontrol tamamlandıktan sonra doğru ise geriye döndürür
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            //Kullanıcıdan alınan girişlerin hatalı olup olmadığının kontrolü ve ekrana hata mesajı yazdırılması
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Hatalı Giris!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Lütfen E-posta Adresinizi Giriniz!";
                    break;
                case AuthError.MissingPassword:
                    message = "Lütfen Şifrenizi Giriniz!";
                    break;
                case AuthError.WrongPassword:
                    message = "Eksik veya Hatalı Şifre!";
                    break;
                case AuthError.InvalidEmail:
                    message = "Eksik veya Hatalı E-posta!";
                    break;
                case AuthError.UserNotFound:
                    message = "Kullanıcı Bulunamadı";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {

            //Kullanıcı giriş yapabildi ise
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} {1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Giriş Başarılı!";
            StartCoroutine(LoadUserData());

            yield return new WaitForSeconds(2);

            usernameField.text = User.DisplayName;
            UIManager.instance.MeassureScreen();
            confirmLoginText.text = "";
            ClearLoginFields();
            ClearRegisterFields();

        }
    }

    //Kullanıcılar için üye olma işlemleri
    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            
            //Kullanıcı adı girildi mi?? kontolü yapılır. Yanlışsa uyarıyı gösterir
            warningRegisterText.text = "Kullanıcı Adınızı Giriniz";
        }
        else if(passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            
            //şifrenin ikinci kez girilmesinin kontrolü yapılır. Yanlışsa uyarıyı gösterir
            warningRegisterText.text = "Şifreler Eşleşmiyor!";
        }
        else 
        {
            
            //E-posta ve şifreyi ileten Firebase auth oturum açma işlevinin çağırılması. (Asenkron olarak Firebase database'deki kayıtlı kullanıvı kontrolü)
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            
            //Kontrol tamamlandıktan sonra doğru ise geriye döndürür

            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                
                //Hatalı girişler için ekranada verilen uyarılar
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Lütfen E-posta Adresinizi Kontrol Ediniz!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Eksik veya Hatalı E-posta";
                        break;
                    case AuthError.MissingPassword:
                        message = "Eksik veya Hatalı Şifre";
                        break;
                    case AuthError.WeakPassword:
                        message = "Zayıf Şifre";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Bu E-posta ile daha önce kayıt olunmuş";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                
                //Kullanıcı oluşturuldu. 
                User = RegisterTask.Result;

                if (User != null)
                {
                    
                    //Kullanıcının girdiği kullanıcı adını atama
                    UserProfile profile = new UserProfile{DisplayName = _username};

                    
                    //Firebase kullanıcı profilini, yeni kayıt olan kullanıcıyı burada oluştur ve güncelle.
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);
                    if(ProfileTask.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {

                        
                        //Kullanıcı oluşturuldu ve Giriş yapma ekranına dön işlevi çağırıldı.
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                        ClearLoginFields();
                        ClearRegisterFields();
                    }
                   
                    

                }

            }
        }
    }

    //Authentication için username güncelleme fonksiyonu
    private IEnumerator UpdateUsernameAuth(string _username)
    {
        UserProfile profile = new UserProfile { DisplayName = _username };

        var ProfileTask = User.UpdateUserProfileAsync(profile);

        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if(ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with { ProfileTask.Exception}");

        }
        else
        {
            //Auth username is now updated
        }
    }

    //Realtime Database için username güncelleme fonksiyonu
    private IEnumerator UpdateUsernameDatabase(string _username)
    {

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with { DBTask.Exception}");

        }
        else
        {
            //Database  username is now updated
        }
    }

    //Realtime Database boy ölçüsü alınması için oluşturulan fonksiyon
    private IEnumerator UpdateSize(int _size)
    {

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("size").SetValueAsync(_size);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with { DBTask.Exception}");

        }
        else
        {
            //Size is now updated
        }
    }

    //Realtime Database bel ölçüsü alınması için oluşturulan fonksiyon
    private IEnumerator UpdateWaist(int _waist)
    {

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("waist").SetValueAsync(_waist);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with { DBTask.Exception}");

        }
        else
        {
            //waist is now updated
        }
    }

    //Realtime Database göğüs ölçüsü alınması için oluşturulan fonksiyon
    private IEnumerator UpdateChest(int _chest)
    {

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("chest").SetValueAsync(_chest);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with { DBTask.Exception}");

        }
        else
        {
            //chest is now updated
        }
    }


    //Realtime Database kalça ölçüsü alınması için oluşturulan fonksiyon
    private IEnumerator UpdateHip(int _hip)
    {

        var DBTask = DBreference.Child("users").Child(User.UserId).Child("hip").SetValueAsync(_hip);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with { DBTask.Exception}");

        }
        else
        {
            //hip is now updated
        }
    }

    //Realtime Database'den Giriş yapıpı ölçülerini daha önce kaydeden kullanıcıların verilerinin çağırılması için oluşturulan fonksiyon

    private IEnumerator LoadUserData()
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with { DBTask.Exception}");
        }

        else if (DBTask.Result.Value == null)
        {
            sizeField.text = "165";
            waistField.text = "80";
            chestField.text = "75";
            hipField.text = "80";

        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            sizeField.text = snapshot.Child("size").Value.ToString();
            waistField.text = snapshot.Child("waist").Value.ToString();
            chestField.text = snapshot.Child("chest").Value.ToString();
            hipField.text = snapshot.Child("hip").Value.ToString();

            
        }

        
    }


}
