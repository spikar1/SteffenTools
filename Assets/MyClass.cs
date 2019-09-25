//namespaces: "bibliotekene" vi kan bruke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public: access modifier, hvem/hva kan få tilgang til klassen
//class: dette er en klasse
//MyClass: navnet på denne klassen, må samsvare med filnavn
//MonoBehaviour: Dette betyr at MyClass arver alt som hører til klassen "MonoBehaviour"
//{: Curly Brackets definerer "scope". 
public class MyClass : MonoBehaviour
{
    //her lager vi en variabel som kan holde en referanse av typen Rigidbody2D
    //merk at variabelen hittil ikke har noen verdi, og vil returnere "null"
    Rigidbody2D rb;

    //public: samme som med klassen, dette er en "access modifier". Dette medbringer at health vises i inspectoren.
    //int: heltall.
    //= 3: Her setter vi en standard verdi, men denne kan endres i inspectoren.
    public int health = 3;

    //Start: En funksjon som blir "fyrt av" fra Unity.
    //dette skjer når et gameObject blir aktivert
    //void: return type. Såfremt det står void, betyr det at funksjonen ikke returnerer noe
    void Start()
    {
        //GetComponent: en funksjon som sjekker GameObjectet som denne klassen er festet på (Component)
        //etter en component av typen <RigidBody2D>.
        //Om den ikke finner noe vil "rb" fremdeles ha en verdi av null, ellers vil den ha referanse til komponenten.
        rb = GetComponent<Rigidbody2D>();
    }

    //Update: Samme som med Start, men blir avfyrt hver eneste frame(tick)
    void Update()
    {
        //if: en statement. if == hvis. alt mellom curlybrackets {} vil skje om if statement returnerer true (bool).
        //i dette tilfellet tilkaller vi funksjonen GetKeyDown som returnerer true om man har trykt en knapp. Her Space
        //Input: en klasse, class.
        //KeyCode: enum. kort fortalt: definerer hvilken knapp man må trykke
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //alt her skjer om if statementen over er true
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            //alt her skjer om forrige if-statement er false, og om man slipper space
        }
        else
        {
            //om begge forrige if statements er false, skjer dette uansett
        }
    }
}
