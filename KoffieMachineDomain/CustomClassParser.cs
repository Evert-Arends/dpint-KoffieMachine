using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace KoffieMachineDomain
{
    public class RootObject
    {
        public ObservableCollection<CustomCoffee> coffees { get; set; }
    }
    public class CustomClassParser
    {
        public RootObject GetCustomClasses()
        {
            string json = @"{ 'coffees': [ { 'Name': 'Spanish Coffee', 'Price': 2, 'steps': [ 'Adding cream', 'Adding cointreau...', 'Adding cognac...', 'Adding suiker...', 'Adding koffie...' ] }, { 'Name': 'Irish Coffee', 'Price': 3, 'steps': [ 'Adding cream', 'Adding whisky...', 'Adding suiker...', 'Adding koffie...' ] }, { 'Name': 'Italian Coffee', 'Price': 1, 'steps': [ 'Adding cream', 'Adding Amaretto...', 'Adding suiker...', 'Adding koffie...' ] } ] }";
            RootObject custom = JsonConvert.DeserializeObject<RootObject>(json);
            return custom;
        }
    }
}
