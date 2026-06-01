import random


class Card:
    def __init__(self, rank, suit):
        self.rank = rank
        self.suit = suit

    def display(self):
        return self.rank + " " + self.suit

    def value(self):
        if self.rank in ["J", "Q", "K"]:
            return 10
        elif self.rank == "A":
            return 11
        else:
            return int(self.rank)


class Deck:
    def __init__(self):
        self.cards = []

        suits = ["Kier", "Karo", "Trefl", "Pik"]
        ranks = ["A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"]

        for suit in suits:
            for rank in ranks:
                self.cards.append(Card(rank, suit))

    def shuffle(self):
        random.shuffle(self.cards)

    def draw_card(self):
        return self.cards.pop()


class Hand:
    def __init__(self):
        self.cards = []

    def add_card(self, card):
        self.cards.append(card)

    def calculate_value(self):
        total = 0
        aces = 0

        for card in self.cards:
            total += card.value()

            if card.rank == "A":
                aces += 1

        while total > 21 and aces > 0:
            total -= 10
            aces -= 1

        return total

    def display(self):
        for card in self.cards:
            print(card.display())

        print("Wartość ręki:", self.calculate_value())


class Player:
    def __init__(self, name):
        self.name = name
        self.hand = Hand()


class Dealer:
    def __init__(self):
        self.hand = Hand()


def main():
    deck = Deck()
    deck.shuffle()

    player = Player("Gracz")
    dealer = Dealer()

    player.hand.add_card(deck.draw_card())
    player.hand.add_card(deck.draw_card())

    dealer.hand.add_card(deck.draw_card())
    dealer.hand.add_card(deck.draw_card())

    print("____BLACKJACK____")

    while True:
        print("\nTwoje karty:")
        player.hand.display()

        if player.hand.calculate_value() > 21:
            print("Przegrałeś. Masz więcej niż 21.")
            return

        choice = input("Dobierasz kartę? hit/stand: ")

        if choice == "hit":
            player.hand.add_card(deck.draw_card())
        elif choice == "stand":
            break
        else:
            print("Wpisz hit albo stand.")

    print("\nKarty dealera:")
    dealer.hand.display()

    while dealer.hand.calculate_value() < 17:
        print("\nDealer dobiera kartę.")
        dealer.hand.add_card(deck.draw_card())
        dealer.hand.display()

    player_value = player.hand.calculate_value()
    dealer_value = dealer.hand.calculate_value()

    print("\n=== WYNIK ===")
    print("Gracz:", player_value)
    print("Dealer:", dealer_value)

    if dealer_value > 21:
        print("Wygrałeś. Dealer ma więcej niż 21.")
    elif player_value > dealer_value:
        print("Wygrałeś.")
    elif player_value < dealer_value:
        print("Przegrałeś.")
    else:
        print("Remis.")


if __name__ == "__main__":
    main()
