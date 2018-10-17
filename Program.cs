using System;
using System.Collections.Generic;

namespace JoppesAnimalKeeper
{
    class Program
    {
        static void Main(string[] args)
        {
            //program starts and instanciate a new Pet Owner , that own one or more pets.
            PetOwner joppe = new PetOwner();
            joppe.Menu();
        }
    }

    public class PetOwner
    {
        private int _age;
        private List<Animal> _pets = new List<Animal>();
        private Ball ball = new Ball();
        private int _option;
  
        public void Menu()
        {
            Console.Clear();
            //menu that will give options for the user and switch case
            //ask and store Petowner age.
            Console.WriteLine("How old are you buddy?");
            _age = Convert.ToInt32(Console.ReadLine());
            //store animals
            Console.WriteLine("How many pets do you have? : ");
            var numOfAnimals = Convert.ToInt32(Console.ReadLine());
            for (var i = 0; i < numOfAnimals; i++)
            {
                // Ask for what type of Animal they want (1-3) (enum animalType)
              
                Console.WriteLine("tell me more about those Pets!");
                Console.WriteLine("1: it is a dog");
                Console.WriteLine("2: it is a puppy");
                Console.WriteLine("3: it is a cat");
                // Read input 
                var animalType = Convert.ToInt32(Console.ReadLine());
                if (animalType > 3 || animalType < 1)
                {
                    Console.WriteLine("There is no such a choise!");
                    Console.ReadKey();
                    Menu();
                }
                
                switch (animalType)
                {
                    case 1:
                    {
                        Animal animal = new Dog();
                        // Fill in all animal informations Name, Breed etc.
                        Console.WriteLine($"What's the name of the dog #{i + 1}?");
                        Console.ReadLine();
                        Console.WriteLine($"How old is your dog #{i + 1}?");
                        Console.ReadLine();
                        Console.WriteLine($"What's your dog favorite food #{i + 1}?");
                        Console.ReadLine();
                        Console.WriteLine($"What's your dog breed #{i + 1}?");
                        Console.ReadLine();
                        //add the pet to the animal list
                        _pets.Add(animal);
                        break;
                    }
                    case 2:
                    {
                        Animal animal = new Puppy();
                        // Fill in all animal informations Name, Breed etc.
                        Console.WriteLine($"What's the name of the puppy #{i + 1}?");
                        Console.ReadLine();
                        Console.WriteLine($"How many months your puppy #{i + 1}?");
                        Console.ReadLine();
                        Console.WriteLine($"What's your puppy favorite food #{i + 1}?");
                        Console.ReadLine();
                        Console.WriteLine($"What's your pet breed #{i + 1}?");
                        Console.ReadLine();
                        //add the pet to the animal list
                        _pets.Add(animal);
                        break;
                    }
                    case 3:
                    {
                        Animal animal = new Cat();
                        // Fill in all animal informations Name, Breed etc.
                        Console.WriteLine($"What's the name of the cat #{i + 1}?");
                        Console.ReadLine();
                        Console.WriteLine($"How old is your cat #{i + 1}?");
                        Console.ReadLine();
                        Console.WriteLine($"What's your cat favorite food #{i + 1}?");
                        Console.ReadLine();
                        Console.WriteLine($"What's your cat breed #{i + 1}?");
                        Console.ReadLine();
                        //add the pet to the animal list
                        _pets.Add(animal);
                        break;
                    }

                    default:
                        Console.Write("Invalid Option. Press any key to return");
                        Console.ReadKey(true);
                        Menu();
                        break;
                }
            }
            
            ToDo();

            void ToDo()
            {
                //List the animal and ask what the user wants to do.
                List_animals();
                Console.WriteLine(""); //empty line
                Console.WriteLine("What would like to do with your pet!");
                // 3 opitions play,feed,groom (implementation here is possible)
                Console.WriteLine("1: Play with a pet");
                Console.WriteLine("2: Feed a pet");
                Console.WriteLine("3: Groom a pet");
                Console.WriteLine("4: Exit Program");
                _option = Convert.ToInt32(Console.ReadLine());
                switch (_option)
                {
                    case 1:

                        Play();
                        break;

                    case 2:
                        Feed();
                        break;

                    case 3:
                        Shower_animal();
                        Groom_animal();
                        Trim_nails();
                        Console.WriteLine("Press enter to return");
                        Console.ReadKey();
                        ToDo();
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.Write("Invalid Option. Press any key to return");
                        Console.ReadKey(true);
                        ToDo();
                        break;
                }
            }
        }

        public void List_animals()
        {
                //display pets that the person own, everytime is called
                foreach (var pets in _pets)
                {
                    //this will call the method that has the pet to string 
                    pets.PetInfoToString();
                }
                //printing empty list even if have a for loop with index over pet list. 
        }

        public void Play()
        {
            //play with a Pet
            //but first check if the ball is fine to play or need to buy a new one.
            Console.WriteLine("Checking if the Play Ball is fine to play!");
            Check_ball(ball:ball);
        }

        public void Feed()
        {
            Console.Clear();
            List_animals();
            Console.WriteLine("Wich pet you want to feed?");
            int animalIndex = Convert.ToInt32(Console.ReadLine());
            //feed an animal calling the method eat()
            _pets[animalIndex - 1].Eat();
           //also need to send the food to the animal imparameter from list_animal() ???
        }
        
        public void Fetch()
        {
            Console.WriteLine("Wich pet would like to play");
            List_animals();
            //choose pet from the list
            int animalIndex = Convert.ToInt32(Console.ReadLine());
            _pets[animalIndex - 1].Interact(ball:ball);
            //play fetch
            Console.WriteLine("You played with the Pet! The pet is Happy now.");
            
            //ball quality drop
            Menu();
        }

        public void Check_ball(Ball ball)
        {
            //check the quality of the playing ball
            Console.WriteLine("its fine , lets play!");
            Fetch();
            ball.Order_online();
            
        }

        public void Shower_animal()
        {
            Console.WriteLine("You Showered your Pet!");
            //ishowered = true;
        }

        public void Groom_animal()
        {
            Console.WriteLine("You toss your pet, it looks nice!");
            //isGroomed = true;
        }

        public void Trim_nails()
        {
            Console.WriteLine("You trimmed your pet nails!");
            //IsTrimed = true ;
        }
    }

    public class Ball
    {
        
        private string _colour;
        private int _quality;
        
        public Ball (string colour = "red")
        {
            this._colour = colour;
        }

       
        //the ball loses durability after being used by calling lower quality method.
        public void Lower_quality(int quality)
        {
            this._quality = quality - 1 ;
        }

        public void Order_online()
        {
            Console.WriteLine("The quality of the ball was bad.");
            Console.WriteLine("So you got a new ball to play");
            _quality = 10;
        }
    }

    public enum AnimalType
    {
        Dog = 1,
        Puppy = 2,
        Cat = 3
    }

    public abstract class Animal
    {
        protected int Age { get; set; }
        protected float Months { get; set; }
        protected string Name { get; set; }
        protected string Favfood { get; set; }
        protected string Breed { get; set; }
        protected bool IsHungry = true;
        protected bool IsShowered { get; set; }
        protected bool IsGroomed { get; set; }
        protected bool IsTrimed { get; set; }

        public virtual void Interact(Ball ball)
        {
            Hungry_animal();
            if (IsHungry == false)
            {
                Console.WriteLine("You interacting  with a Pet!");
            }

        }
        public void Eat()
        {
            
            Console.WriteLine("You Feed a Pet!");
            IsHungry = false;
            
        }

        public virtual void Hungry_animal()
        {
            if (IsHungry)
            {
                Console.WriteLine("Pet makes weird noises and don't want to play");
            }
        }

        public abstract void PetInfoToString();

    }

    public class Dog : Animal
    {

        public Dog()
        {

        }
        //dog class constructor 
        public Dog (string name, int age, string favfood ,string breed,bool ishungry ,bool isShowered,bool isGroomed,bool isTrimed)
        {
            this.Name = name;
            this.Age = age;
            this.Favfood = favfood;
            this.Breed = breed;
            this.IsHungry = ishungry;
            this.IsShowered = isShowered;
            this.IsGroomed = isGroomed;
            this.IsTrimed = isTrimed;
        }
   
        public override void Interact(Ball ball)
        {
            
            if (IsHungry)
            {
                Console.WriteLine("Dog refuse to play, keep licking your hand instead!");
            }
            else
            {
                Console.WriteLine("You played fetch with a dog!\n the dog is happy!");
                //lower quality in 2
                ball.Lower_quality(2);
            }
            
        }

        public override void PetInfoToString()
        {
            Console.WriteLine("\n\nName: {0}\n" +
                              "Pet Age: {1}\n" +
                              "Favorite Food: {2}\n" +
                              "Breed: {3}\n" +
                              "This pet is hungry : {4}\n",
                Name, Age, Favfood, Breed, IsHungry);
        }

    }

    public class Puppy : Dog
    {
        public Puppy()
        {
            
        }
        //puppy class constructor 
        public Puppy(string name, int months, string favfood, string breed, bool ishungry, bool isShowered, bool isGroomed, bool isTrimed)
        {
            this.Name = name;
            this.Months = months;
            this.Favfood = favfood;
            this.Breed = breed;
            this.IsHungry = ishungry;
            this.IsShowered = isShowered;
            this.IsGroomed = isGroomed;
            this.IsTrimed = isTrimed;

        }
        public override void Interact(Ball ball)
        {
            Console.WriteLine("You played fetch with a puppy,it took the toy and run from you!");
            //lower quality in 1
            ball.Lower_quality(1);
        }
        public override void PetInfoToString()
        {
            Console.WriteLine("\n\nName: {0}\n" +
                              "Puppy Months: {1}\n" +
                              "Favorite Food: {2}\n" +
                              "Breed: {3}\n" +
                              "This pet is hungry : {4}\n",
                Name, Age, Favfood, Breed, IsHungry);
        }
    }

    public class Cat : Animal
    {
        public Cat()
        {
            
        }
        //cat class constructor 
        public Cat(string name, int age, string favfood, string breed, bool ishungry, bool isShowered, bool isGroomed, bool isTrimed)
        {
            this.Name = name;
            this.Age = age;
            this.Favfood = favfood;
            this.Breed = breed;
            this.IsHungry = ishungry;
            this.IsShowered = isShowered;
            this.IsGroomed = isGroomed;
            this.IsTrimed = isTrimed;
        }
     
        public override void Hungry_animal()
        {
            if (IsHungry)
            {
                Console.WriteLine("Pet makes weird noises and grumpy face!");
                //feed the cat or he will find mouse situation
                //here the favorite food was suposed to be used inparameter Cat.favfood
                Console.WriteLine("Feed your cat a treat!\n yes/no");
                string feedcat = Console.ReadLine();
                if (feedcat == "yes")
                {
                    Eat();
                }
                else if(feedcat == "no")
                {
                    Console.WriteLine("your cat found a mouse!");
                }
              
            }
            else
            {
                Console.WriteLine("The cat Meows!! and is passing under your legs");
                
            }
        }
        public override void PetInfoToString()
        {
            Console.WriteLine("\n\nName: {0}\n" +
                              "Pet Age: {1}\n" +
                              "Favorite Food: {2}\n" +
                              "Breed: {3}\n" +
                              "This pet is hungry : {4}\n",
                Name, Age, Favfood, Breed, IsHungry);
        }
    }
}
