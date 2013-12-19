using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nitrogen.Wumbalo.ViewModels
{
	public abstract class Inpc : INotifyPropertyChanged
	{
		private Dictionary<string, object> _properties = new Dictionary<string, object>();

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = "")
		{
			if ( PropertyChanged != null )
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetField<T> (ref T field, T value, string propertyName, bool overrideChecks = false)
		{
			if ( !overrideChecks )
				if ( EqualityComparer<T>.Default.Equals(field, value) )
					return false;

			field = value;
			OnPropertyChanged(propertyName);

			return true;
		}

		protected void Set (object value, [CallerMemberName] string propertyName = "")
		{
			var property = this.GetType().GetProperty(propertyName);
			var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

			if ( _properties.ContainsKey(propertyName) )
			{
				_properties[propertyName] = value;
			}
			else
			{
				_properties.Add(propertyName, value);
			}

			OnPropertyChanged(propertyName);
		}

		protected TProperty Get<TProperty> ([CallerMemberName] string propertyName = "")
		{
			if ( _properties.ContainsKey(propertyName) )
				return (TProperty) _properties[propertyName];
			else
				return default(TProperty);
		}

		protected dynamic Get ([CallerMemberName] string propertyName = "")
		{
			if ( _properties.ContainsKey(propertyName) )
				return _properties[propertyName];
			else
			{
				var type = GetType().GetProperty(propertyName).PropertyType;
				if ( type.IsValueType )
					return Activator.CreateInstance(type);

				return null;
			}
		}
	}
}
