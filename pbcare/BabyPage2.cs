﻿using System;

using Xamarin.Forms;

namespace pbcare
{
	public class BabyPage2 : ContentPage
	{
		public BabyPage2 ()
		{
			
			ListView childrenList = new ListView {
				RowHeight = 40
			};
			 


			childrenList.ItemsSource = pbcareApp.u.MyChilren;
			childrenList.ItemTemplate = new DataTemplate (typeof(TextCell));
			childrenList.ItemTemplate.SetBinding (TextCell.TextProperty, "name");

			childrenList.ItemSelected +=  (Sender, Event) => {
				
			};

			var AddChild = new Button {
				Text = "إضافة طفـــل",
				TextColor = Color.White,
				FontSize = 15,
				BackgroundColor = Color.Red,
				VerticalOptions = LayoutOptions.End
			};

			AddChild.Clicked += (sender, e) => {
				Navigation.PushAsync(new AddBaby());
			};

			Content = new StackLayout {
				VerticalOptions= LayoutOptions.FillAndExpand,
				Padding = 20 ,
				Children = {  
					childrenList ,
					AddChild
				}
			};

		}

	}
}


