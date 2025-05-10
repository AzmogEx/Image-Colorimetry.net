using JLV_TOOLS;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Program {
    static void Main(string[] args) {

        //chat4(); //affiche 4 chat en 1 image
        //copierImage(); copie l'image de base dans un autre tableau
        //imageRgb();
        //inversion_image();
        //inversion_image_180();
        //rotation_image90();
        //choix_inversion();
    }

    static void copierImage() {
        int[,] Mon_Image = new int[100,100];
        Mon_Image[50,50] = 0x555555;
        Console.WriteLine($"{Mon_Image[50,50]:X}");

        IMAGE.Grille_Vers_BMP(Mon_Image,"test.bmp");


        int[,] Image = IMAGE.BMP_Vers_Grille("test.bmp");
        Console.WriteLine($"{Image[50,50]:X}");

        int[,] meme = IMAGE.BMP_Vers_Grille("meme.png");
        int[,] Copie_meme = new int[100,100];

        Copie_meme = meme;
        Copie_meme[50,50] = 0x000000;

        Console.WriteLine($"{Copie_meme[100,100]:x}");

        IMAGE.Grille_Vers_BMP(Copie_meme,"Copie_meme.png");

        //int largeur = Image.GetLength(0); avoir la largeur
        //int hauteur = Image.GetLength(1)); avoir la hauteur
    }
    static void chat4() {

        int[,] Image = IMAGE.BMP_Vers_Grille("meme.png");

        int Largeur = Image.GetLength(0);
        int Hauteur = Image.GetLength(1);


        int[,] Copie_Image = new int[Largeur,Hauteur];

        int demi_largeur = Largeur / 2;
        int demi_hauteur = Hauteur / 2;

        Console.WriteLine($"Taille: {Hauteur}x{Largeur}");

        //avoir 4 fois l'image
        for(int X = 0; X < demi_largeur; X++) {
            for(int Y = 0; Y < demi_hauteur; Y++) {
                Copie_Image[X,Y] = Image[2 * X,2 * Y];
                Copie_Image[X + demi_largeur,Y] = Copie_Image[X,Y];

                Copie_Image[X,Y + demi_hauteur] = Image[2 * X,2 * Y];
                Copie_Image[X + demi_largeur,Y + demi_hauteur] = Copie_Image[X,Y];

            }
        }

        IMAGE.Grille_Vers_BMP(Copie_Image,"Copie_meme.png");


        Process.Start("meme.png");
        Process.Start("Copie_meme.png");

    }
    static void imageRgb() {
        int[,] Image = IMAGE.BMP_Vers_Grille("meme.png");

        int Largeur = Image.GetLength(0);
        int Hauteur = Image.GetLength(1);


        int[,] Copie_Image = new int[Largeur,Hauteur];

        int demi_largeur = Largeur;
        int demi_hauteur = Hauteur;

        Console.WriteLine($"Taille: {Hauteur}x{Largeur}");

        int moyenne;

        int[,] couche_Rouge = new int[Largeur,Hauteur];
        int[,] couche_verte = new int[Largeur,Hauteur];
        int[,] couche_bleu = new int[Largeur,Hauteur];
        int[,] couche_gris = new int[Largeur,Hauteur];

        byte R;
        byte G;
        byte B;

        for(int X = 0; X < demi_largeur; X++) {
            for(int Y = 0; Y < demi_hauteur; Y++) {

                IMAGE.Entier_Vers_RGB(Image[X,Y],out R,out G,out B);
                moyenne = (R + G + B) / 3;

                couche_Rouge[X,Y] = IMAGE.RGB_Vers_Entier(R,0,0);
                couche_verte[X,Y] = IMAGE.RGB_Vers_Entier(0,G,0);
                couche_bleu[X,Y] = IMAGE.RGB_Vers_Entier(0,0,B);
                couche_gris[X,Y] = IMAGE.RGB_Vers_Entier(moyenne,moyenne,moyenne);
            }
        }
        IMAGE.Grille_Vers_BMP(couche_Rouge,"-Rouge.png");
        IMAGE.Grille_Vers_BMP(couche_verte,"-Vert.png");
        IMAGE.Grille_Vers_BMP(couche_bleu,"-Bleu.png");
        IMAGE.Grille_Vers_BMP(couche_gris,"-Gris.png");


        Process.Start("meme.png");
        Process.Start("-Rouge.png");
        Process.Start("-Vert.png");
        Process.Start("-Bleu.png");
        Process.Start("-Gris.png");
    }

    static void inversion_image() {
        int[,] Image = IMAGE.BMP_Vers_Grille("meme.png");

        int Largeur = Image.GetLength(0);
        int Hauteur = Image.GetLength(1);


        int[,] Copie_Image = new int[Largeur,Hauteur];


        Console.WriteLine($"Taille: {Hauteur}x{Largeur}");

        for(int X = 0; X < Largeur; X++) {
            for(int Y = 0; Y < Hauteur; Y++) {
                int nouvelle_colonne = Largeur - X - 1;
                int nouvelle_ligne = Y;
                Copie_Image[nouvelle_colonne,nouvelle_ligne] = Image[X,Y];
            }
        }

        IMAGE.Grille_Vers_BMP(Copie_Image,"Copie_meme.png");

        Process.Start("meme.png");
        Process.Start("Copie_meme.png");

    }
    static void inversion_image_180() {
        int[,] Image = IMAGE.BMP_Vers_Grille("meme.png");

        int Largeur = Image.GetLength(1);
        int Hauteur = Image.GetLength(0);


        int[,] Copie_Image = new int[Hauteur,Largeur];

        Console.WriteLine($"Taille: {Hauteur}x{Largeur}");

        for(int X = 0; X < Largeur; X++) {
            for(int Y = 0; Y < Hauteur; Y++) {
                int nouvelle_colonne = Largeur - X - 1;
                int nouvelle_ligne = Y;
                Copie_Image[nouvelle_ligne,nouvelle_colonne] = Image[Y,X];
            }
        }

        IMAGE.Grille_Vers_BMP(Copie_Image,"Copie_meme.png");

        Process.Start("meme.png");
        Process.Start("Copie_meme.png");

    }

    static void rotation_image90() {
        int[,] Image = IMAGE.BMP_Vers_Grille("meme.png");

        int Largeur = Image.GetLength(0);
        int Hauteur = Image.GetLength(1);


        int[,] Copie_Image = new int[Hauteur,Largeur];

        Console.WriteLine($"Taille: {Hauteur}x{Largeur}");

        for(int X = 0; X < Largeur; X++) {
            for(int Y = 0; Y < Hauteur; Y++) {
                int nouvelle_colonne = Largeur - X - 1;
                int nouvelle_ligne = Y;
                Copie_Image[nouvelle_ligne,nouvelle_colonne] = Image[X,Y];
            }
        }

        IMAGE.Grille_Vers_BMP(Copie_Image,"Copie_meme.png");

        Process.Start("meme.png");
        Process.Start("Copie_meme.png");

    }
    //static void choix_inversion() {
    //    Console.WriteLine("Comment souhaitez vous tourner l'image ?");
    //    Console.WriteLine("90, 128 ?");
    //    int choix = 0;

    //    bool OK = int.TryParse(Console.ReadLine(),out choix);

    //    if(OK == true) {
    //        if (90) {
    //            rotation_image90();
    //        }
    //    }
    //    else {
    //        Console.WriteLine("Erreur");
    //        return;
    //    }

    //}

}
