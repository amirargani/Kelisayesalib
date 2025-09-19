using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Messages
{
    public class faMessage
    {
        // APPClinet
        public const string Login = " - ورود به سایت";
        public const string Register = " - ثبت نام در سایت";
        public const string Forgot = " - فراموشی رمز عبور";
        public const string ResetPassword = " - بازیابی رمز عبور";
        public const string TitleLogin = "ورود به سایت";
        public const string TitleRegister = "ثبت نام در سایت";
        public const string TitleForgot = "فراموشی رمز عبور";
        public const string TitleResetPassword = "بازیابی رمز عبور";
        public const string With = "یا با دیگر اکانت وارد شوید!";
        public const string MessageHome = "خـانه";
        public const string Map = "نقشه";
        public const string TitleMap = "نقشه گوگل";
        public const string Close = "بستن";
        public const string MessageStartFirst = "کار را با";
        public const string MessageStartLast = "شروع کنید که می تواند همه چیز را در مورد کتاب مقدس یاد بگیرید.";
        public const string MessageForgot = "پس از وارد کردن ایمیل خود لینک تغییر رمز عبور به ایمیل شما ارسال می شود.";
        public const string MessageResetPassword = "لطفاً اطلاعات خواسته شده را با دقت وارد کنید.";
        public const string ButtonLogin = "ورود";
        public const string ButtonRegister = "ثبت نام";
        public const string ButtonSubmit = "ارسال";
        public const string ButtonForgot = "بازیابی رمز عبور";
        public const string MyAccountText = "حساب من";
        public const string DashboardText = "پنل کاربری";
        public const string LoginText = "ورود";
        public const string RegisterText = "ثبت نام";
        public const string ButtonLogout = "خروج از حساب کاربری";
        // APPClinet
        // ApplicationUser
        public const string Email = "ایمیل";
        public const string NewEmail = "ایمیل جدید";
        public const string Password = "رمز عبور";
        public const string CurrentPassword = "رمز عبور فعلی";
        public const string NewPassword = "رمز عبور جدید";
        public const string RememberMe = "مرا بخاطر بسپار؟";
        public const string ConfirmPassword = "تکرار رمز عبور";
        public const string NewConfirmPassword = "تکرار رمز عبور جدید";
        public const string ForgotPassword = "رمز عبور خود را فراموش کرده ام!";
        public const string RequiredMsg = "لطفا فیلد {0} را وارد کنید.";
        public const string RequiredMsgMax = "{0} باید بیشتر از {2} کاراکتر باشد.";
        public const string RequiredMsgInfo = "اطلاعات وارد شده معتبر نمی باشد.";
        public const string RequiredMsgEmail = "لطفا فیلد {0} را وارد کنید.";
        public const string RequiredMsgEmailAddress = "{0} وارد شده معتبر نمی باشد.";
        public const string RequiredMsgEmailAddressExpression = "{0} را به درستی وارد کنید.";
        public const string RequiredMsgPassword = "لطفا فیلد {0} را وارد کنید.";
        public const string RequiredMsgPasswordStringLength = "{0} باید بیشتر از {2} کاراکتر باشد.";
        public const string RequiredMsgPasswordCompare = "{0} باید با رمز عبور مطابقت داشته باشد.";
        public const string RequiredMsgNewPasswordCompare = "{0} باید با رمز عبور جدید مطابقت داشته باشد.";
        public const string RequiredMsgFirstName = "لطفا فیلد {0} را وارد کنید.";
        public const string RequiredMsgLastName = "لطفا فیلد {0} را وارد کنید.";
        public const string RequiredMsgFirstNameStringLength = "{0} باید بیشتر از {2} کاراکتر باشد.";
        public const string RequiredMsgLastNameStringLength = "{0} باید بیشتر از {2} کاراکتر باشد.";
        // Menu
        public const string Menu = "منو";
        public const string MenusBack = "منوها";
        public const string MenuEvent = "رویداد";
        public const string MenuTeam = "تـیـم مـا";
        public const string MenuGallery = "گـالــری";
        public const string MenuNew = "اخـبـار";
        public const string MenuCourse = "آمـوزش";
        public const string MenuAbout = "دربـاره مـا";
        // Menu
        // Categories & SubCategories & Details
        public const string Title = "عنوان";
        public const string TitleCategory = "نام دسته";
        public const string TitleSubCategory = "نام زیر دسته";
        public const string SelectFile = "انتخاب فرمت فایل";
        public const string Text = "متن";
        public const string Image = "تصویر";
        public const string Images = "تصاویر";
        public const string Video = "ویدیو";
        public const string Videos = "ویدیوها";
        public const string Prev = "صفحه قبل";
        public const string Next = "صفحه بعد";
        public const string ImageProfile = "تصویر شخص:";
        public const string ImageBackground = "تصویر پس زمینه:";
        public const string SelectImage = "یک تصویر را انتخاب کنید.";
        public const string SizeImage = "حجم تصویر باید زیر ۱.۱ مگابایت باشد.";
        public const string SelectImageAfter = "تصویر انتخاب شده:";
        public const string SelectVideo = "یک ویدیو را انتخاب کنید.";
        public const string SizeVideo = "حجم ویدیو باید زیر ۳۵۰.۱ مگابایت باشد.";
        public const string SelectVideoAfter = "ویدیو انتخاب شده:";
        public const string Service = "خدمت";
        public const string Description = "توضحیات";
        public const string FontName = "آیکون";
        public const string Active = "فعال";
        public const string Inactive = "غیر فعال";
        public const string Edit = "ویرایش";
        public const string Detail = "جزئیات";
        public const string Plus = "+";
        public const string Minus = "-";
        public const string ButtonActivePassive = "فعال/غیر فعال کردن";
        public const string Telegram = "تلگرام";
        public const string Youtube = "یوتیوب";
        public const string Instagram = "اینستاگرام";
        public const string Twitter = "توییتر";
        public const string Facebook = "فیس بوک";
        public const string Website = "سایت";
        public const string YoutubeLink = "یوتیوب لینک";
        public const string StartAt = "زمان شروع";
        public const string StopAt = "زمان پایان";
        public const string TimeEvent = "ساعت شروع";
        public const string Countdown = "تاریخ شمارش معکوس";
        public const string EmbedLinkGoogleMap = "پیوند قراردادن نقشه گوگل";
        public const string MostVisited = "پربازدیدترین های";
        public const string TheNewest = "جدیدترین های";
        public const string CoursesText = "آمـوزش";
        public const string NewsText = "اخبار";
        public const string GalleryText = "گـالــری‌";
        public const string Footer = "پاورقی";
        public const string Newsletter = "خبرنامه";
        public const string Address = "ویرایش آدرس سایت";
        public const string SocialNetworks = "ویرایش شبکه های اجتماعی";
        public const string Street = "خیابان";
        public const string Number = "پلاک";
        public const string PostCode = "کد پستی";
        public const string City = "شهر";
        public const string Country = "کشور";
        public const string Share = "به اشتراک گذاری لینک";

        public const string Categories = "دسته ها";
        public const string CategoriesManage = "مدیریت";
        public const string CategoriesForExample = "بطور مثال:";
        public const string CategoriesBackView = "نمایش دسته ها";
        public const string CategoryAdd = "دسته جدید";
        public const string CategoryEdit = "ویرایش دسته";
        public const string CategoriesView = "نمایش دسته ها";
        public const string CategorySelect = "انتخاب دسته";
        public const string CategoryMessageAdd = "دسته جدید با موفقیت ثبت شد.";
        public const string CategoryMessageEdit = "ویرایش اطلاعات با موفقیت ثبت شد.";
        public const string CategoryErrroMessage = "عملیات مورد نظر با موفقیت ثبت نشد.";
        public const string CategoriesNotfound = "اطلاعات درخواست شده یافت نشد.";
        public const string SubCategories = "زیر دسته ها";
        public const string SubCategoriesManage = "مدیریت";
        public const string SubCategoriesBackView = "نمایش زیر دسته ها";
        public const string SubCategoryAdd = "زیر دسته جدید";
        public const string SubCategoryEdit = "ویرایش زیر دسته";
        public const string SubCategoriesView = "نمایش زیر دسته ها";
        public const string SubCategorySelect = "انتخاب زیر دسته";
        public const string SubCategoryMessageAdd = "زیر دسته جدید با موفقیت ثبت شد.";
        public const string SubCategoryMessageEdit = "ویرایش اطلاعات با موفقیت ثبت شد.";
        public const string SubCategoryErrroMessage = "عملیات مورد نظر با موفقیت ثبت نشد.";
        public const string SubCategoriesNotfound = "اطلاعات درخواست شده یافت نشد.";
        public const string DetailAdd = "جزئیات جدید";
        public const string DetailEdit = "ویرایش جزئیات";
        public const string DetailView = "نمایش جزئیات زیر دسته ها";
        public const string And = "و";
        public const string ImageAdd = "تصویر جدید";
        public const string ImageEdit = "ویرایش تصویر";
        public const string ImageDetailView = "نمایش جزئیات تصاویر";
        public const string VideoAdd = "ویدیو جدید";
        public const string VideoEdit = "ویرایش ویدیو";
        public const string VideoDetailView = "نمایش جزئیات ویدیو";
        public const string Copyright = "تمامی حقوق مادی و معنوی این سایت متعلق به کــلیســـای صلـیـب می باشد و هرگونه کپی برداری غیرقانونی محسوب خواهد شد.";


        // Categories & SubCategories & Details
        // ApplicationUser
        // Email & Password
        public const string SubjectConfirmEmail = "فعال سازی حساب کاربری";
        public const string SubjectConfirmEmailActivationLink = "لینک جدید فعال سازی حساب کاربری";
        public const string SubjectResstPassword = "بازیابی رمز عبور";
        public const string SubjectResstPasswordNewLink = "لینک جدید بازیابی رمز عبور";
        public const string DescriptionWelcome = "به کلیسای صلیب خوش آمدید";
        public const string DescriptionResstPasswordLink = "لینک بازیابی رمز عبور";
        public const string TextAccountActivation = "فعال سازی حساب کاربری";
        public const string TextResstPasswordActivation = "بازیابی رمز عبور";
        public const string TextConfirmEmail = "از اینکه وقت خود را برای ثبت نام در کلیسای صلیب صرف کردید، متشکریم! بعد از ایجاد یک حساب لازم است، ایمیل خود را تایید کنید!!! تا حساب کاربری شما فعال شود و پس از فعال سازی می توانید وارد حساب کاربری خود شوید.";
        public const string TextConfirmEmailActivationLink = "این لینک جدید فعالسازی حساب کاربری شما است. لطفاً بر روی آن کلیک کنید و ایمیل خود را تایید کنید!!! تا حساب کاربری شما فعال شود و پس از فعال سازی می توانید وارد حساب کاربری خود شوید.";
        public const string TextResstPassword = "با کلیک روی لینک بازیابی رمز عبور شما می توانید، به صفحه رمز عبور جدید بروید و رمز عبور جدید را در آنجا وارد کنید.";
        public const string TextActivationLink = "لینک از زمان ارسال این ایمیل به مدت ۷ روز فعال خواهد بود.";
        public const string RegisterConfirmationTitle = "تأیید ثبت نام";
        public const string ConfirmEmailTitle = "تأیید ایمیل";
        public const string VerificationEmailTitle = "تأیید صحت ایمیل";
        public const string RegistrationErrorTitle = "خـطـا در ثبت نام";
        public const string RegistrationActiveTitle = "ثبت نام فعال";
        public const string ForgotConfirmationTitle = "تأیید رمز عبور";
        public const string TextPageButton = "نمایش صفحه";
        public static string TemplateEmail = Directory.GetCurrentDirectory() + "\\faTemplates\\Email.html";
        public static string TemplateNewsletter = Directory.GetCurrentDirectory() + "\\faTemplates\\Newsletter.html";
        public const string WebsiteMainzLogo = "lib/images/dist/logos/logo.png";
        public const string WebsiteKelisayesalib = "lib/images/dist/mainz/mainz.jpg";
        // Email & Password
        // User & Admin
        public const string UserProfile = "مشخصات کاربری";
        public const string ChangePassword = "تغییر رمز عبور";
        public const string ChangeEmail = "تغییر ایمیل";
        public const string Settings = "تنظیمات";
        public const string Menus = "منوها";
        public const string FirstName = "نام";
        public const string LastName = "نام خانوادگی";
        public const string FirstNameEN = "نام به انگلیسی";
        public const string LastNameEN = "نام خانوادگی به انگلیسی";
        public const string PhoneNumber = "شماره موبایل";
        public const string PrivateNumber = "شماره تلفن";
        public const string ButtonSave = "ذخیره کردن";
        public const string AddTitle = "";
        public const string DeleteTitle = "";
        public const string ErrorTitle = "";
        public const string UpdateTitle = "ذخیره سازی با موفقیت انجام شد.";
        public const string InfoTitle = "";
        public const string PasswordTitle = "رمز عبور جدید با موفقیت ثبت شد.";
        public const string EmailTitle = "ایمیل جدید با موفقیت ثبت شد.";
        public const string EmailUnknownTitle = "ایمیل مورد نظر در دسترس نیست.";
        public const string EmailUnchangedTitle = "ایمیل شما قبلاً ثبت شده است.";
        public const string EmailConfirmEmailTitle = "کاربر گرامی ثبت نام شما با موفقیت انجام شد.";
        public const string EmailConfirmEmailErrorTitle = "خطا در ثبت نام.";
        public const string EmailLoginPasswordErrorTitle = "رمز عبور شما اشتباه است.";
        public const string EmailConfirmEmailActiveErrorTitle = "لطفاً ایمیل خود را بررسی کنید." + " " + "حساب کاربری شما غیر فعال است.";
        public const string EmailRegistrationActiveTitle = "کاربر گرامی ایمیل شما قبلاً تایید شده است.";
        public const string EmailRegistrationActiveErrorTitle = "خطا در فعال کردن ایمیل.";


        // User & Admin
    }
}
