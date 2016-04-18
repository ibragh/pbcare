﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace pbcare
{
	public class SettingPage : ContentPage
	{
		ListView setting = new ListView {
			RowHeight = 50  
		};

		public SettingPage ()
		{
			this.Title = "الإعدادات";
			BackgroundColor = Color.FromRgb (94, 196, 225);
			List<settingClass> settingList = new List<settingClass> ();	
			settingList.Add (new settingClass ("تغيير الاسم", 1));
			settingList.Add (new settingClass("تغيير كلمة المرور" , 2));
			settingList.Add (new settingClass("جهاز الإستشعار" ,3 ));

			setting.ItemsSource = settingList ;
			setting.ItemTemplate = new DataTemplate (typeof(everyCell));
			setting.BackgroundColor = Color.Transparent ;

			setting.ItemSelected += (Sender, Event) => {
				var selceted = (settingClass)Event.SelectedItem;
				var settingView = new settingView (selceted);
				 Navigation.PushAsync (settingView);
			};


			var logOutButton = new Button {
				Text = " تسجيل خــــروج ",
				Image = "logOut.png",
				TextColor = Color.White,
				FontSize = 15,
				WidthRequest = 200,
				HeightRequest = 65,
				BackgroundColor = Color.Red ,
				VerticalOptions = LayoutOptions.End ,
				FontAttributes = FontAttributes.Bold ,
				BorderRadius = 30 ,
				BorderWidth = 4
			};

			logOutButton.Clicked += OnAlertYesNoClicked;

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(10 ,20 , 10 , 53) ,
				Children = {  
					setting,
					logOutButton
				}
			};

		}

		async void OnAlertYesNoClicked (object sender, EventArgs e)
		{
			var answer = await DisplayAlert ("تسجيل الخروج ", "هل تريد تأكيد تسجيل الخروج ؟ ", "نعم", "لا");
			Debug.WriteLine ("Answer: " + answer);

			if (answer == true) {
				pbcareApp.Database.InsertUserLoggedin (false);
				pbcareApp.IsUserLoggedIn = false; 
				await Navigation.PopToRootAsync();
				await Navigation.PushModalAsync (new pbcareMainPage());
			} 
		}
	}

	//--------------------------------------------------------------
	public class settingClass 
	{
		public string name { get; set;}
		public int number { get; set;}
		public settingClass(string n , int num ){
			this.name = n;
			this.number = num;
		}
	}
//===========================================================
	public class everyCell : ViewCell
	{
		public everyCell(){
			
			var name = new Label {
				Text = " . ",
				TextColor = Color.White,
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Start,


			};
			name.SetBinding (Label.TextProperty, "name");

			View = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.EndAndExpand,
				Padding = new Thickness (15, 5, 15, 15),

				Children = {
					new StackLayout {
						Padding = new Thickness (15, 5, 0, 15),
						Orientation = StackOrientation.Vertical,
						HorizontalOptions = LayoutOptions.EndAndExpand,
						Children = { name 
						},


					}
				}
			};
		}
	}

	//-------------------------------------------------------------

	public class settingView : ContentPage
	{
		public settingView (settingClass selectedSetting)
		{
			BackgroundColor = Color.FromRgb (94, 196, 225);
			Title = selectedSetting.name ;

			var CancelButton = new Button {
				Text = "إلغاء",
			};

			CancelButton.Clicked += (sender, e) => {
				Navigation.PopAsync ();
			};
			if(selectedSetting.number == 1){
				var yourname = new Label {
					Text = "اسمك : ",
					FontSize = 20,
					HorizontalOptions = LayoutOptions.End,
					TextColor = Color.White
				};

				var nameEntry = new Entry1 { Placeholder = "أدخل اسمك هنا" };
				var saveNameButton = new Button { Text = " حفظ البيانات " 
				};
				
				saveNameButton.Clicked += (sender, e) => {
					if (!string.IsNullOrWhiteSpace(nameEntry.Text)) {
						pbcareApp.u.name = nameEntry.Text;
						Navigation.PopAsync ();
						DisplayAlert ("  تم", "  تغيير الاسم إلى" + nameEntry.Text, "موافق");
	
					} else {
						DisplayAlert ("خطأ", "لم يتم إدخال أي اسم", "إلغاء");
	
					}
				};


				this.Content = new ScrollView {
					Content = new StackLayout {
						VerticalOptions = LayoutOptions.FillAndExpand,
						Padding = 20 ,
						Children = { yourname, nameEntry, saveNameButton, CancelButton }
					}
				};
			}
			else if(selectedSetting.number == 2){
				var yourPass = new Label {
					Text = "كلمة المرور : ",
					FontSize = 20,
					HorizontalOptions = LayoutOptions.End,
					TextColor = Color.White
				};
				var confYourPass = new Label {
					Text = "تأكيد كلمة المرور",
					FontSize = 20,
					HorizontalOptions = LayoutOptions.End,
					TextColor = Color.White
				};
				var passwordEntry = new Entry1 {	Placeholder = "أدخل كلمة المرور هنا" };
				var passConfirm = new Entry1 { Placeholder = " تأكيد كلمة المرور" };
				var savePassButton = new Button {
					Text = "حفظ البيانات"
				};
	
				savePassButton.Clicked += (sender, e) => {
					if (!string.IsNullOrWhiteSpace(passwordEntry.Text)&& passConfirm.Text.Equals (passwordEntry.Text)) {
						Navigation.PopAsync ();
						DisplayAlert (" تم", " تغيير كلمة المرور ", "موافق");
	
					} else if (passwordEntry.Text != passConfirm.Text) {
						DisplayAlert ("خطأ", "كلمة المرور غير متطابقة", "إلغاء");
	
					} else{
						DisplayAlert ("خطأ", "كلمة المرور غير متطابقة", "إلغاء");
					}
				};

				this.Content = new ScrollView {
					Content = new StackLayout {
						VerticalOptions = LayoutOptions.FillAndExpand,
						Padding = 20,
						Children = { yourPass, passwordEntry,confYourPass, passConfirm, savePassButton, CancelButton }
					}
				};
			}

			else if(selectedSetting.number == 3){
				
			}

		}
	}
}



