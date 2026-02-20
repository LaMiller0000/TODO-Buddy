using System;

namespace TODOBuddy;

public class BuddyClass
{
    
    private int id;
    private string name;
    private string description;
    private int health;
    private Behaviours behaviour;
    
    public BuddyClass(int id) =>
    (this.id, name, description, health) = (id, "John", "A TODO Buddy", 10);
    
    public BuddyClass(int id, string name, string description, int health) =>
    (this.id, this.name, this.description, this.health) = (id, name, description, health);
    
    public int getId() => id;
    public string getName() => name;
    public string getDescription() => description;
    public int getHealth() => health;
    public void setId(int id) => this.id = id;
    public void setName(string name) => this.name = name;
    public void setDescription(string description) => this.description = description;
    public void setHealth(int health) => this.health = health;

    public void chechHealth(Behaviours behaviour)
    {
        switch (health)
        {
            case >= 10:
                setBehaviour(Behaviours.HAPPY);
                break;
            case >= 5 when health < 10:
                setBehaviour(Behaviours.IDLE);
                break;
            case >= 1 when health < 5:
                setBehaviour(Behaviours.HURT_IDLE);
                break;
            case 0:
                setBehaviour(Behaviours.DEAD);
                break;
            default:
                Console.WriteLine("No valid health!");
                break;
        }
    }

    public void getHurt(int damage = 0)
    {
        if (this.health > 0)
        {
            this.health -= damage;
        }
    }
    
    public void eat(int food = 0) // would describe what happens when eat is called such as an eating animation
    {
        if (this.health > 0)
        {
            this.health += food;
        }
    }

    public void death() // would describe what happens when death is called such as a death animation.
    {
        
    }
    
    public void setBehaviour(Behaviours behaviour) => this.behaviour = behaviour;
}