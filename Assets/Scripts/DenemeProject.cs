using System;
using UnityEngine;

public class DenemeProject
{
    public string name;
    public DenemeProject(string x)
    {
        this.name = x;
    }
}


public class DenemeProject2 : MonoBehaviour
{
    public void AMethod()
    {
        DenemeProject deneme1 = new DenemeProject("cc");
    }

    private void Awake()
    {
        AMethod();
    }
}


public class DenemeProject3 : MonoBehaviour
{
    private void Start()
    {
    }
}







































public class Character
{
    private string Name { get; set; }
    private int Id { get; set; }
    private bool IsActive { get; set; }

    public Character(string name, int id, bool isActive)
    {
        Name = name;
        Id = id;
        IsActive = isActive;
    }
}


public class Enemy : MonoBehaviour
{
    private void Awake()
    {
        var enemy = new Character("Enemy1", 1, false);
    }
}

public class Player : Character
{
    public Player(string name, int id, bool isActive) : base(name, id, isActive)
    {
        
    }
}
