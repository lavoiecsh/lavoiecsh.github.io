---
layout: post
title: "Jour 2: Inventory Management System"
lang: fr
ref: inventory_management_system
date: 2018-12-02 9:00:00 -0400
categories: [advent]
tags: [advent]
---
Nous avons donc le premier problème qui nous demande de travailler sur des chaînes de caractères.

La première partie nous demande de construire un algorithme de checksum simple pour savoir si tous les identifiants des boîtes sont corrects. J'ai opté pour une solution qui utilise presque uniquement du LINQ en C#.

{% highlight C# %}
var counts = ids.Select(s => s.GroupBy(c => c).Select(g => g.Count()))
    .Select(cs =>
    {
        var csl = cs.ToList();
        return new[] {csl.Contains(2), csl.Contains(3)};
    })
    .ToList();
return (counts.Count(c => c[0]) * counts.Count(c => c[1])).ToString();
{% endhighlight %}

Le premier ```Select``` retourne le nombre de fois on voit chaque caractère dans chaque identifiant. Le second ```Select``` retourne si ces comptes contiennent 2 et 3, voulant dire que l'identifiant contient un caractère deux fois ou contient un caractère trois fois. La dernière partie fait juste multiplier le nombre d'identifiants contenant un multiple de 2 par le nombre d'identifiants contenant un multiple de 3.


La seconde partie nous demande de trouver les deux boîtes similaires en trouvant les deux identifiants qui ne diffèrent que par 1 caractère. Ici j'ai opté pour une solution à la "force brute" pour vérifier toutes les paires possible et retourner la première qui répond aux critères.

{% highlight C# %}
foreach (var id1 in ids)
{
    foreach (var id2 in ids)
    {
        if (id1 == id2)
            continue;
        var commonLetters = string.Concat(id1.Zip(id2, (c1, c2) => c1 == c2 ? c1 : ' ').Where(c => c != ' '));
        if (commonLetters.Length == id1.Length - 1)
            return commonLetters;
    }
}
{% endhighlight %}

J'ai utilisé du LINQ ici aussi pour calculer les caractères identiques. Si les deux caractères sont identiques, je le retourne, sinon je le remplace par un espace que je retire par la suite. La première paire qui a une lettre commune de moins que le nombre de lettres dans l'identifiant est celle que nous voulons. J'avais peur que rouler un algorithme ```O(n²)``` prendrait trop de temps à calculer, mais l'input est assez petite que je n'ai pas eu besoin d'optimiser.