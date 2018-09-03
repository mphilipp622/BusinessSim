using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyLot
{
    float _price;

    bool _forSale, _isDeveloped;

    string _name;

    Vector3 _location;

    //Player _owner;
    public float price
    {
        get
        {
            return _price;
        }
        set
        {
            _price = value;
        }
    }

    public bool forSale
    {
        get
        {
            return _forSale;
        }
        set
        {
            _forSale = value;
        }
    }

    public bool isDeveloped
    {
        get
        {
            return _isDeveloped;
        }
        set
        {
            _isDeveloped = value;
        }
    }

    public string name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public Vector3 location
    {
        get
        {
            return _location;
        }
        set
        {
            _location = value;
        }
    }

    public EmptyLot()
    {
        // default constructor
        price = 0;
        forSale = true;
        isDeveloped = false;
    }

    public EmptyLot (float newPrice, bool isForSale, bool isAlreadyDeveloped, string newName, Vector3 newLocation)
    {
        price = newPrice;
        forSale = isForSale;
        isDeveloped = isAlreadyDeveloped;
        name = newName;
        location = newLocation;
    }
}
