﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum AnimalType //TODO: achieve using inheritence
// {
//     PREY,
//     PREDATOR
// }

public class AnimalsManager : MonoBehaviour
{
    private static AnimalsManager instance;
    public static AnimalsManager Instance { get { return instance; } }

    [SerializeField] private GameObject preyAnimalPrefab;
    [SerializeField] private GameObject predatorAnimalPrefab;
    [SerializeField] private Transform animalsParent;
    [SerializeField] private GameObject animalUIPrefab;
    [SerializeField] private Transform animalsUIParent;
    private List<Animal> animals;
    private List<AnimalUI> animalUIs;

    private void Start()
    {
        animals = new List<Animal>();
        animalUIs = new List<AnimalUI>();
        // NewAnimal();
    }

    private void NewAnimal(GameObject animalPrefab)
    {
        GameObject newAnimalGameObj = Instantiate(animalPrefab, animalsParent);
        Animal newAnimal = newAnimalGameObj.GetComponent<Animal>();
        GameObject newAnimalUIGameObj = Instantiate(animalUIPrefab, animalsUIParent);
        AnimalUI newAnimalUI = newAnimalUIGameObj.GetComponent<AnimalUI>();

        newAnimal.Init(newAnimalUI);
        newAnimalUIGameObj.GetComponent<UIFollowTarget>().Init(newAnimalGameObj.transform);

        animals.Add(newAnimal);
        animalUIs.Add(newAnimalUI);
    }

    public void NewPrey()
    {
        NewAnimal(preyAnimalPrefab);
    }

    public void NewPredator()
    {
        NewAnimal(predatorAnimalPrefab);
    }
}
