using System;
using System.Collections.Generic;

class Card
{
    public string Rank;
    public string Suit;

    public Card(string rank, string suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public string Display()
    {
        return Rank + " " + Suit;
    }

    public int Value()
    {
        if (Rank == "J" || Rank == "Q" || Rank == "K")
        {
            return 10;
        }
        else if (Rank == "A")
        {
            return 11;
        }
        else
        {
            return int.Parse(Rank);
        }
    }
}

class Deck
{
    public List<Card> Cards = new List<Card>();
    Random random = new Random();

    public Deck()
    {
        string[] suits = { "Kier", "Karo", "Trefl", "Pik" };
        string[] ranks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        for (int i = 0; i < suits.Length; i++)
        {
            for (int j = 0; j < ranks.Length; j++)
            {
                Cards.Add(new Card(ranks[j], suits[i]));
            }
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            int randomIndex = random.Next(Cards.Count);

            Card temp = Cards[i];
            Cards[i] = Cards[randomIndex];
            Cards[randomIndex] = temp;
        }
    }

    public Card DrawCard()
    {
        Card card = Cards[0];
        Cards.RemoveAt(0);
        return card;
    }
}

class Hand
{
    public List<Card> Cards = new List<Card>();

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public int CalculateValue()
    {
        int total = 0;
        int aces = 0;

        foreach (Card card in Cards)
        {
            total += card.Value();

            if (card.Rank == "A")
            {
                aces++;
            }
        }

        while (total > 21 && aces > 0)
        {
            total -= 10;
            aces--;
        }

        return total;
    }

    public void Display()
    {
        foreach (Card card in Cards)
        {
            Console.WriteLine(card.Display());
        }

        Console.WriteLine("Wartość ręki: " + CalculateValue());
    }
}

class Player
{
    public string Name;
    public Hand Hand;

    public Player(string name)
    {
        Name = name;
        Hand = new Hand();
    }
}

class Dealer
{
    public Hand Hand;

    public Dealer()
    {
        Hand = new Hand();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Deck deck = new Deck();
        deck.Shuffle();

        Player player = new Player("Gracz");
        Dealer dealer = new Dealer();

        player.Hand.AddCard(deck.DrawCard());
        player.Hand.AddCard(deck.DrawCard());

        dealer.Hand.AddCard(deck.DrawCard());
        dealer.Hand.AddCard(deck.DrawCard());

        Console.WriteLine("____BLACKJACK____");

        while (true)
        {
            Console.WriteLine("\nTwoje karty:");
            player.Hand.Display();

            if (player.Hand.CalculateValue() > 21)
            {
                Console.WriteLine("Przegrałeś. Masz więcej niż 21.");
                return;
            }

            Console.Write("Dobierasz kartę? hit/stand: ");
            string choice = Console.ReadLine();

            if (choice == "hit")
            {
                player.Hand.AddCard(deck.DrawCard());
            }
            else if (choice == "stand")
            {
                break;
            }
            else
            {
                Console.WriteLine("Wpisz hit albo stand.");
            }
        }

        Console.WriteLine("\nKarty dealera:");
        dealer.Hand.Display();

        while (dealer.Hand.CalculateValue() < 17)
        {
            Console.WriteLine("\nDealer dobiera kartę.");
            dealer.Hand.AddCard(deck.DrawCard());
            dealer.Hand.Display();
        }

        int playerValue = player.Hand.CalculateValue();
        int dealerValue = dealer.Hand.CalculateValue();

        Console.WriteLine("\n=== WYNIK ===");
        Console.WriteLine("Gracz: " + playerValue);
        Console.WriteLine("Dealer: " + dealerValue);

        if (dealerValue > 21)
        {
            Console.WriteLine("Wygrałeś. Dealer ma więcej niż 21.");
        }
        else if (playerValue > dealerValue)
        {
            Console.WriteLine("Wygrałeś.");
        }
        else if (playerValue < dealerValue)
        {
            Console.WriteLine("Przegrałeś.");
        }
        else
        {
            Console.WriteLine("Remis.");
        }
    }
}
