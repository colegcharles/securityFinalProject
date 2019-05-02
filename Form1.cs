using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imageConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // initilize image as global variable
        Image img = Image.FromFile("C:\\Users\\Admin\\Desktop\\lena_gray.bmp");




        public static Bitmap[] Create4Shares()
        {
            Image img = Image.FromFile("C:\\Users\\Admin\\Desktop\\lena_gray.bmp");
            // create bitmap object from image for the set/getpixel functions
            Bitmap image = new Bitmap(img);

            // create the byte array from the image
            byte[] byteArray = new byte[262144];
            var count = 0;
            for (var i = 0; i < image.Height; i++)
            {
                for (var j = 0; j < image.Width; j++)
                {
                    byteArray[count] = image.GetPixel(i, j).R;
                    count++;
                }
            }

            // create the bit array from the byte array
            var userABits = new System.Collections.BitArray(byteArray);
            var userBBits = new System.Collections.BitArray(byteArray);
            var userCBits = new System.Collections.BitArray(byteArray);
            var userDBits = new System.Collections.BitArray(byteArray);

            // convert the bit arrays to randomize shares
            count = 0;
            Random rnd = new Random();
            for (int i = 0; i < userABits.Length; i++)
            {
                if (count == 0 || count == 1)
                {
                    if (rnd.Next(1, 100) > 50)
                    {
                        userBBits[i] = true;
                        userCBits[i] = true;
                        userDBits[i] = true;
                    }
                    else
                    {
                        userBBits[i] = false;
                        userCBits[i] = false;
                        userDBits[i] = false;
                    }
                }
                else if (count == 2 || count == 3)
                {
                    if (rnd.Next(1, 100) > 50)
                    {
                        userABits[i] = true;
                        userCBits[i] = true;
                        userDBits[i] = true;
                    }
                    else
                    {
                        userABits[i] = false;
                        userCBits[i] = false;
                        userDBits[i] = false;
                    }
                }
                else if (count == 4 || count == 5)
                {
                    if (rnd.Next(1, 100) > 50)
                    {
                        userABits[i] = true;
                        userBBits[i] = true;
                        userDBits[i] = true;
                    }
                    else
                    {
                        userABits[i] = false;
                        userBBits[i] = false;
                        userDBits[i] = false;
                    }
                }
                else
                {
                    if (rnd.Next(1, 100) > 50)
                    {
                        userABits[i] = true;
                        userBBits[i] = true;
                        userCBits[i] = true;
                    }
                    else
                    {
                        userABits[i] = false;
                        userBBits[i] = false;
                        userCBits[i] = false;
                    }
                }

                count++;
                if (count == 8)
                {
                    count = 0;
                }
            }


            // try to put back together the image
            var newBits = new System.Collections.BitArray(byteArray);
            count = 0;
            for (var i = 0; i < newBits.Length; i++)
            {
                if (count == 0 || count == 1)
                {
                    newBits[i] = userABits[i];
                }
                else if (count == 2 || count == 3)
                {
                    newBits[i] = userBBits[i];
                }
                else if (count == 4 || count == 5)
                {
                    newBits[i] = userCBits[i];
                }
                else if (count == 6 || count == 7)
                {
                    newBits[i] = userDBits[i];
                }
                count++;
                if (count == 8)
                {
                    count = 0;
                }
            }

            // convert the user shares back to byte arrays
            byte[] userABytes = BitArrayToByteArray(userABits);
            byte[] userBBytes = BitArrayToByteArray(userBBits);
            byte[] userCBytes = BitArrayToByteArray(userCBits);
            byte[] userDBytes = BitArrayToByteArray(userDBits);
            byte[] originalBack = BitArrayToByteArray(newBits);

            //
            Bitmap userAImage = new Bitmap(img);
            Bitmap userBImage = new Bitmap(img);
            Bitmap userCImage = new Bitmap(img);
            Bitmap userDImage = new Bitmap(img);
            Bitmap userFake1Image = new Bitmap(img);
            Bitmap userFake2Image = new Bitmap(img);
            Bitmap originalBacks = new Bitmap(img);

            count = 0;
            for (var i = 0; i < img.Width; i++)
            {
                for (var k = 0; k < img.Height; k++)
                {
                    userAImage.SetPixel(i, k, Color.FromArgb(userABytes[count], userABytes[count], userABytes[count]));
                    userBImage.SetPixel(i, k, Color.FromArgb(userBBytes[count], userBBytes[count], userBBytes[count]));
                    userCImage.SetPixel(i, k, Color.FromArgb(userCBytes[count], userCBytes[count], userCBytes[count]));
                    userDImage.SetPixel(i, k, Color.FromArgb(userDBytes[count], userDBytes[count], userDBytes[count]));
                    userFake1Image.SetPixel(i, k, Color.FromArgb(userDBytes[count] ^ userCBytes[count], userDBytes[count] ^ userCBytes[count], userDBytes[count] ^ userCBytes[count]));
                    userFake2Image.SetPixel(i, k, Color.FromArgb(userDBytes[count] ^ userABytes[count], userDBytes[count] ^ userABytes[count], userDBytes[count] ^ userABytes[count]));
                    originalBacks.SetPixel(i, k, Color.FromArgb(originalBack[count], originalBack[count], originalBack[count]));
                    count++;
                }
            }

            Bitmap[] imageArray = new Bitmap[7];
            imageArray[0] = userAImage;
            imageArray[1] = userBImage;
            imageArray[2] = userCImage;
            imageArray[3] = userDImage;
            imageArray[4] = userFake1Image;
            imageArray[5] = userFake2Image;
            imageArray[6] = originalBacks;
            return imageArray;


        }

        public static Bitmap[] Create2Shares()
        {
            Image img = Image.FromFile("C:\\Users\\Admin\\Desktop\\lena_gray.bmp");
            // create bitmap object from image for the set/getpixel functions
            Bitmap image = new Bitmap(img);

            // create the byte array from the image
            byte[] byteArray = new byte[262144];
            var count = 0;
            for (var i = 0; i < image.Height; i++)
            {
                for (var j = 0; j < image.Width; j++)
                {
                    byteArray[count] = image.GetPixel(i, j).R;
                    count++;
                }
            }

            // create the bit array from the byte array
            var userABits = new System.Collections.BitArray(byteArray);
            var userBBits = new System.Collections.BitArray(byteArray);


            // convert the bit arrays to randomize shares
            count = 0;
            Random rnd = new Random();
            for (int i = 0; i < userABits.Length; i++)
            {
                if (count == 0 || count == 1 || count == 2 || count == 3)
                {
                    if (rnd.Next(1, 11) > 5)
                    {
                        userBBits[i] = true;
                    }
                    else
                    {
                        userBBits[i] = false;
                    }
                }
                else
                {
                    if (rnd.Next(1, 11) > 5)
                    {
                        userABits[i] = true;
                    }
                    else
                    {
                        userABits[i] = false;
                    }
                }

                count++;
                if (count == 8)
                {
                    count = 0;
                }
            }


            // try to put back together the image
            var newBits = new System.Collections.BitArray(byteArray);
            count = 0;
            for (var i = 0; i < newBits.Length; i++)
            {
                if (count == 0 || count == 1 || count == 3 || count == 4)
                {
                    newBits[i] = userABits[i];
                }
                else
                {
                    newBits[i] = userBBits[i];
                }

                count++;
                if (count == 8)
                {
                    count = 0;
                }
            }

            // convert the user shares back to byte arrays
            byte[] userABytes = BitArrayToByteArray(userABits);
            byte[] userBBytes = BitArrayToByteArray(userBBits);
            byte[] originalBack = BitArrayToByteArray(newBits);

            //
            Bitmap userAImage = new Bitmap(img);
            Bitmap userBImage = new Bitmap(img);
            Bitmap userCImage = new Bitmap(img);
            Bitmap originalBacks = new Bitmap(img);

            count = 0;
            for (var i = 0; i < img.Width; i++)
            {
                for (var k = 0; k < img.Height; k++)
                {
                    userAImage.SetPixel(i, k, Color.FromArgb(userABytes[count], userABytes[count], userABytes[count]));
                    userBImage.SetPixel(i, k, Color.FromArgb(userBBytes[count], userBBytes[count], userBBytes[count]));
                    userCImage.SetPixel(i, k, Color.FromArgb(userBBytes[count] ^ userABytes[count], userBBytes[count] ^ userABytes[count], userBBytes[count] ^ userABytes[count]));
                    originalBacks.SetPixel(i, k, Color.FromArgb(originalBack[count], originalBack[count], originalBack[count]));
                    count++;
                }
            }

            Bitmap[] ImageArray = new Bitmap[3];
            ImageArray[0] = userAImage;
            ImageArray[1] = userBImage;
            ImageArray[2] = userCImage;


            return ImageArray;
        }
        public static byte[] BitArrayToByteArray(System.Collections.BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

        private void buttonGenerateShares_Click(object sender, EventArgs e)
        {
            if (radioButtonShare2.Checked == true)
            {
                Bitmap[] Shares = Create2Shares();

                pictureBox2.Image = Shares[0];
                pictureBox3.Image = Shares[2];

                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
            }
            if (radioButtonShare4.Checked == true)
            {
                Bitmap[] Shares = Create4Shares();

                pictureBox2.Image = Shares[0];
                pictureBox3.Image = Shares[1];
                pictureBox4.Image = Shares[5];
                pictureBox5.Image = Shares[4];
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
            }

            radioButtonCBC.Enabled = true;
            radioButtonEBC.Enabled = true;
            buttonEncrypt.Enabled = true;
            groupBox2.Enabled = true;
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {

            //if (radioButtonShare2.Checked == true)
            //{
            //    Image imgShare1 = pictureBox2.Image;
            //    Image imgShare2 = pictureBox3.Image;

            //    byte[] share1EncryptionBytes = EncryptImage(imgShare1);
            //    byte[] share2EncryptionBytes = EncryptImage(imgShare2);

            //    //for (var i = 0; i< share1EncryptionBytes.Length; i++)
            //    //{
            //    //    Console.WriteLine(share1EncryptionBytes[i]);
            //    //}


            //    Bitmap share1EncryptedImage = new Bitmap(imgShare1);
            //    Bitmap share2EncryptedImage = new Bitmap(imgShare2);

            //    var count2 = 0;
            //    for (var i = 0; i < share1EncryptedImage.Height; i++)
            //    {
            //        for (var j = 0; j < share1EncryptedImage.Width; j++)
            //        {
            //            share1EncryptedImage.SetPixel(i, j, Color.FromArgb(share1EncryptionBytes[count2], share1EncryptionBytes[count2], share1EncryptionBytes[count2]));
            //            share2EncryptedImage.SetPixel(i, j, Color.FromArgb(share2EncryptionBytes[count2], share2EncryptionBytes[count2], share2EncryptionBytes[count2]));
            //            //Console.WriteLine(share1EncryptionBytes[count2]);
            //            count2++;
            //        }
            //    }

            //    pictureBox7.Image = share1EncryptedImage;
            //    pictureBox8.Image = share2EncryptedImage;

            //}
            //else
            //{
            //    Image imgShare1 = pictureBox2.Image;
            //    Image imgShare2 = pictureBox3.Image;
            //    Image imgShare3 = pictureBox4.Image;
            //    Image imgShare4 = pictureBox5.Image;

            //    byte[] share1EncryptionBytes = EncryptImage(imgShare1);
            //    byte[] share2EncryptionBytes = EncryptImage(imgShare2);
            //    byte[] share3EncryptionBytes = EncryptImage(imgShare3);
            //    byte[] share4EncryptionBytes = EncryptImage(imgShare4);

            //    Bitmap share1EncryptedImage = new Bitmap(imgShare1);
            //    Bitmap share2EncryptedImage = new Bitmap(imgShare2);
            //    Bitmap share3EncryptedImage = new Bitmap(imgShare3);
            //    Bitmap share4EncryptedImage = new Bitmap(imgShare4);

            //    var count3 = 0;
            //    for (var i = 0; i < share1EncryptedImage.Height; i++)
            //    {
            //        for (var j = 0; j < share1EncryptedImage.Height; j++)
            //        {
            //            share1EncryptedImage.SetPixel(i, j, Color.FromArgb(share1EncryptionBytes[count3], share1EncryptionBytes[count3], share1EncryptionBytes[count3]));
            //            share2EncryptedImage.SetPixel(i, j, Color.FromArgb(share2EncryptionBytes[count3], share2EncryptionBytes[count3], share2EncryptionBytes[count3]));
            //            share3EncryptedImage.SetPixel(i, j, Color.FromArgb(share3EncryptionBytes[count3], share3EncryptionBytes[count3], share3EncryptionBytes[count3]));
            //            share4EncryptedImage.SetPixel(i, j, Color.FromArgb(share4EncryptionBytes[count3], share4EncryptionBytes[count3], share4EncryptionBytes[count3]));
            //            count3++;
            //        }
            //    }

            //    pictureBox7.Image = share1EncryptedImage;
            //    pictureBox8.Image = share2EncryptedImage;
            //    pictureBox9.Image = share1EncryptedImage;
            //    pictureBox10.Image = share2EncryptedImage;
            //}

            Image img = Image.FromFile("C:\\Users\\Admin\\Desktop\\lena_gray.bmp");
            byte[] plaintextEncryptionBytes = EncryptImage(img);



            Console.WriteLine(plaintextEncryptionBytes[10]);
            Console.WriteLine(512 * 512);

            Bitmap plaintextEncryptedImage = new Bitmap(img);
            var count = 0;
            for (var i = 0; i < plaintextEncryptedImage.Height; i++)
            {
                for (var j = 0; j < plaintextEncryptedImage.Height; j++)
                {
                    plaintextEncryptedImage.SetPixel(i, j, Color.FromArgb(plaintextEncryptionBytes[count], plaintextEncryptionBytes[count], plaintextEncryptionBytes[count]));
                    count++;
                }


            }

            //label2.Visible = true;
            //decryptButton.Enabled = true;

            pictureBox6.Image = plaintextEncryptedImage;

            /////////////////////////////////////////////////////////////////////////
            // temporary code to display image as blurry to show Dr. rasheed the gui will erase after
            /////////////////////////////////////////////////////////////////////////
            //    Bitmap[] Shares = Create4Shares();
            //    if (radioButtonShare4.Checked == true)
            //    {

            //        pictureBox10.Image = Shares[0];
            //        pictureBox8.Image = Shares[4];
            //        pictureBox9.Image = Shares[5];
            //        pictureBox7.Image = Shares[4];
            //    }
            //    else
            //    {
            //        pictureBox6.Image = Shares[1];
            //        pictureBox8.Image = Shares[4];
            //        pictureBox7.Image = Shares[4];
            //    }
            label2.Visible = true;
            decryptButton.Enabled = true;
            //}
        }
        private void decryptButton_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            Image img = Image.FromFile("C:\\Users\\Admin\\Desktop\\lena_gray.bmp");

            pictureBox11.Image = img;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;
            pictureBox6.Image = null;
            pictureBox7.Image = null;
            pictureBox8.Image = null;
            pictureBox9.Image = null;
            pictureBox10.Image = null;
            pictureBox11.Image = null;
            groupBox2.Enabled = false;
            label2.Visible = false;
            label3.Visible = false;
            buttonEncrypt.Enabled = false;
            decryptButton.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public byte[] EncryptImage(Image img)
        {
            // get random byte array online and store as variable 
            //var randomByteStringArray = "217,151,25,115,206,147,139,138,250,3,15,87,128,189,150,249,222,4,157,6,157,134,244,45,,39,143,11,219,166,191,37,,31,106,129,220,129,184,247,63,147,192,137,99,232,161,68,155,206,,43,223,176,49,214,,16,133,53,153,112,17,183,145,29,68,215,79,237,56,229,76,13,239,239,104,69,19,25,47,192,135,85,116,192,7,124,45,143,169,48,153,150,48,231,72,49,232,210,,31,191,24,4";
            byte[] randomByteArray = { 217, 151, 25, 115, 206, 147, 139, 138, 250, 3, 15, 87, 128, 189, 150, 249, 222, 4, 157, 6, 157, 134, 244, 45, 39, 143, 11, 219, 166, 191, 37, 31, 106, 129, 220, 129, 184, 247, 63, 147, 192, 137, 99, 232, 161, 68, 155, 206, 43, 223, 176, 49, 214, 16, 133, 53, 153, 112, 17, 183, 145, 29, 68, 215, 79, 237, 56, 229, 76, 13, 239, 239, 104, 69, 19, 25, 47, 192, 135, 85, 116, 192, 7, 124, 45, 143, 169, 48, 153, 150, 48, 231, 72, 49, 232, 210, 31, 191, 24, 4 };
            byte[] pi = { 1, 4, 1, 5, 9, 2, 6, 5, 3, 5, 8, 9, 7, 9, 3, 2, 3, 8, 4, 6, 2, 6, 4, 3, 3, 8, 3, 2, 7, 9, 5, 0, 2, 8, 8, 4, 1, 9, 7, 1, 6, 9, 3, 9, 9, 3, 7, 5, 1, 0, 5, 8, 2, 0, 9, 7 };
            // create the key byte array
            byte[] keyByteArray = new byte[56];
            for (var i = 0; i < keyByteArray.Length; i++)
            {
                keyByteArray[i] = pi[i];
            }

            // create the IV array
            byte[] IV = new byte[8];
            for (var i = 0; i < 8; i++)
            {
                IV[i] = randomByteArray[i];
            }

            // create original P array
            byte[] OriginalPByteArray = new byte[72];
            for (var i = 0; i < OriginalPByteArray.Length; i++)
            {
                OriginalPByteArray[i] = randomByteArray[i];
            }

            // create final P byte array
            byte[] PByteFinal = new byte[72];
            for (var i = 0; i < keyByteArray.Length; i++)
            {
                PByteFinal[i] = (byte)(OriginalPByteArray[i] ^ keyByteArray[i]);
            }
            for (var i = 0; i < 16; i++)
            {
                PByteFinal[i + 56] = (byte)(OriginalPByteArray[i + 56] ^ keyByteArray[i]);
            }

            // Create SBox Arrays
            string sbox1 = "D1310BA6,98DFB5AC,2FFD72DB,D01ADFB7,B8E1AFED,6A267E96,BA7C9045,F12C7F99,24A19947,B3916CF7,0801F2E2,858EFC16,636920D8,71574E69," +
"A458FEA3,F4933D7E,0D95748F,728EB658,718BCD58,82154AEE,7B54A41D,C25A59B5,9C30D539,2AF26013,C5D1B023,286085F0,CA417918,B8DB38EF,8E79DCB0,603A180E,6C9E0E8B,B01E8A3E,D71577C1,BD314B27,78AF2FDA," +
"55605C60,E65525F3,AA55AB94,57489862,63E81440,55CA396A,2AAB10B6,B4CC5C34,1141E8CE,A15486AF,7C72E993,B3EE1411,636FBC2A,2BA9C55D,741831F6,CE5C3E16,9B87931E,AFD6BA33,6C24CF5C,7A325381,28958677," +
"3B8F4898,6B4BB9AF,C4BFE81B,66282193,61D809CC,FB21A991,487CAC60,5DEC8032,EF845D5D,E98575B1,DC262302,EB651B88,23893E81,D396ACC5," +
"0F6D6FF3,83F44239,2E0B4482,A4842004,69C8F04A,9E1F9B5E,21C66842,F6E96C9A,670C9C61,ABD388F0,6A51A0D2,D8542F68,960FA728,AB5133A3,6EEF0B6C,137A3BE4,BA3BF050,7EFB2A98,A1F1651D,39AF0176,66CA593E," +
"82430E88,8CEE8619,456F9FB4,7D84A5C3,3B8B5EBE,E06F75D8,85C12073,401A449F,56C16AA6,4ED3AA62,363F7706,1BFEDF72,429B023D,37D0D724,D00A1248,DB0FEAD3,49F1C09B,075372C9,80991B7B,25D479D8,F6E8DEF7," +
"E3FE501A,B6794C3B,976CE0BD,04C006BA,C1A94FB6,409F60C4,5E5C9EC2,196A2463,68FB6FAF,3E6C53B5,1339B2EB,3B52EC6F,6DFC511F,9B30952C,CC814544,AF5EBD09,BEE3D004,DE334AFD,660F2807,192E4BB3,C0CBA857,45C8740F,D20B5F39,B9D3FBDB,5579C0BD,1A60320A,D6A100C6,402C7279,679F25FE,FB1FA3CC,8EA5E9F8,DB3222F8,3C7516DF,FD616B15,2F501EC8," +
"AD0552AB,323DB5FA,FD238760,53317B48,3E00DF82,9E5C57BB,CA6F8CA0,1A87562E,DF1769DB,D542A8F6,287EFFC3,AC6732C6,8C4F5573,695B27B0,BBCA58C8,E1FFA35D,B8F011A0,10FA3D98,FD2183B8,4AFCB56C,2DD1D35B," +
"9A53E479,B6F84565,D28E49BC,4BFB9790,E1DDF2DA,A4CB7E33,62FB1341,CEE4C6E8,EF20CADA,36774C01,D07E9EFE,2BF11FB4,95DBDA4D,AE909198," +
"EAAD8E71,6B93D5A0,D08ED1D0,AFC725E0,8E3C5B2F,8E7594B7,8FF6E2FB,F2122B64,8888B812,900DF01C,4FAD5EA0,688FC31C,D1CFF191,B3A8C1AD,2F2F2218,BE0E1777,EA752DFE,8B021FA1,E5A0CC0F,B56F74E8,18ACF3D6,CE89E299,B4A84FE0,FD13E0B7,7CC43B81,D2ADA8D9,165FA266,80957705,93CC7314,211A1477,E6AD2065,77B5FA86,C75442F5,FB9D35CF," +
"EBCDAF0C,7B3E89A0,D6411BD3,AE1E7E49,00250E2D,2071B35E,226800BB,57B8E0AF,2464369B,F009B91E,5563911D,59DFA6AA,78C14389,D95A537F,207D5BA2,02E5B9C5,83260376,6295CFA9,11C81968,4E734A41,B3472DCA,7B14A94A,1B510052,9A532915,D60F573F,BC9BC6E4,2B60A476,81E67400,08BA6FB5,571BE91F,F296EC6B,2A0DD915,B6636521,E7B9F9B6,FF34052E,C5855664,53B02D5D,A99F8FA1,08BA4799,6E85076A";
            string sbox2 = "4B7A70E9,B5B32944,DB75092E,C4192623,AD6EA6B0,49A7DF7D,9CEE60B8,8FEDB266,ECAA8C71,699A17FF,5664526C,C2B19EE1,193602A5,75094C29,A0591340,E4183A3E,3F54989A," +
"5B429D65,6B8FE4D6,99F73FD6,A1D29C07,EFE830F5,4D2D38E6,F0255DC1,4CDD2086,8470EB26,6382E9C6,021ECC5E,09686B3F," +
"3EBAEFC9,3C971814,6B6A70A1,687F3584,52A0E286,B79C5305,AA500737,3E07841C,7FDEAE5C,8E7D44EC,5716F2B8,B03ADA37,F0500C0D,F01C1F04,0200B3FF,AE0CF51A,3CB574B2,25837A58,DC0921BD,D19113F9,7CA92FF6,94324773,22F54701,3AE5E581,37C2DADC,C8B57634,9AF3DDA7,A9446146,0FD0030E,ECC8C73E,A4751E41,E238CD99,3BEA0E2F,3280BBA1,183EB331,4E548B38,4F6DB908,6F420D03,F60A04BF,2CB81290,24977C79,5679B072,BCAF89AF,DE9A771F,D9930810,B38BAE12,DCCF3F2E," +
"5512721F,2E6B7124,501ADDE6,9F84CD87,7A584718,7408DA17,BC9F9ABC,E94B7D8C,EC7AEC3A,DB851DFA,63094366,C464C3D2,EF1C1847,3215D908,DD433B37,24C2BA16,12A14D43,2A65C451,50940002,133AE4DD,71DFF89E," +
"10314E55,81AC77D6,5F11199B,043556F1,D7A3C76B,3C11183B,5924A509,F28FE6ED,97F1FBFA,9EBABF2C,1E153C6E,86E34570,EAE96FB1,860E5E0A,5A3E2AB3,771FE71C,4E3D06FA,2965DCB9,99E71D0F,803E89D6,5266C825," +
"2E4CC978,9C10B36A,C6150EBA,94E2EA78,A5FC3C53,1E0A2DF4,F2F74EA7,361D2B3D,1939260F,19C27960,5223A708,F71312B6,EBADFE6E,EAC31F66,E3BC4595,A67BC883,B17F37D1,018CFF28,C332DDEF,BE6C5AA5,65582185," +
"68AB9802,EECEA50F,DB2F953B,2AEF7DAD,5B6E2F84,1521B628,29076170,ECDD4775,619F1510,13CCA830,EB61BD96,0334FE1E,AA0363CF,B5735C90,4C70A239,D59E9E0B,CBAADE14,EECC86BC,60622CA7,9CAB5CAB,B2F3846E,648B1EAF,19BDF0CA,A02369B9,655ABB50,40685A32,3C2AB4B3,319EE9D5,C021B8F7,9B540B19,875FA099,95F7997E,623D7DA8,F837889A,97E32D77," +
"11ED935F,16681281,0E358829,C7E61FD6,96DEDFA1,7858BA99,57F584A5,1B227263,9B83C3FF,1AC24696,CDB30AEB,532E3054,8FD948E4,6DBC3128,58EBF2EF,34C6FFEA,FE28ED61,EE7C3C73,5D4A14D9,E864B7E3,42105D14," +
"203E13E0,45EEE2B6,A3AAABEA,DB6C4F15,FACB4FD0,C742F442,EF6ABBB5,654F3B1D,41CD2105,D81E799E,86854DC7,E44B476A,3D816250,CF62A1F2,5B8D2646,FC8883A0,C1C7B6A3,7F1524C3,69CB7492,47848A0B,5692B285,095BBF00,AD19489D,1462B174,23820E00,58428D2A,0C55F5EA,1DADF43E,233F7061,3372F092,8D937E41,D65FECF1,6C223BDB,7CDE3759,CBEE7460," +
"4085F2A7,CE77326E,A6078084,19F8509E,E8EFD855,61D99735,A969A7AA,C50C06C2,5A04ABFC,800BCADC,9E447A2E,C3453484,FDD56705,0E1E9EC9,DB73DBD3,105588CD,675FDA79,E3674340,C5C43465,713E38D8,3D28F89E,F16DFF20,153E21E7,8FB03D4A,E6E39F2B,DB83ADF7";
            string sbox3 = "E93D5A68,948140F7,F64C261C,94692934,411520F7,7602D4F7,BCF46B2E,D4A20068,D4082471,3320F46A,43B7D4B7,500061AF,1E39F62E,97244546,14214F74,BF8B8840,4D95FC1D,96B591AF,70F4DDD3,66A02F45,BFBC09EC,03BD9785,7FAC6DD0,31CB8504," +
"96EB27B3,55FD3941,DA2547E6,ABCA0A9A,28507825,530429F4,0A2C86DA,E9B66DFB,68DC1462,D7486900,680EC0A4,27A18DEE,4F3FFEA2,E887AD8C,B58CE006,7AF4D6B6,AACE1E7C,D3375FEC,CE78A399,406B2A42,20FE9E35,D9F385B9,EE39D7AB,3B124E8B,1DC9FAF7,4B6D1856,26A36631,EAE397B2," +
"3A6EFA74,DD5B4332,6841E7F7,CA7820FB,FB0AF54E,D8FEB397,454056AC,BA489527,55533A3A,20838D87,FE6BA9B7,D096954B,55A867BC,A1159A58,CCA92963,99E1DB33,A62A4A56,3F3125F9,5EF47E1C,9029317C,FDF8E802,04272F70,80BB155C,05282CE3,95C11548,E4C66D22,48C1133F,C70F86DC,07F9C9EE,41041F0F,404779A4,5D886E17,325F51EB,D59BC0D1,F2BCC18F," +
"41113564,257B7834,602A9C60,DFF8E8A3,1F636C1B,0E12B4C2,02E1329E,AF664FD1,CAD18115,6B2395E0,333E92E1,3B240B62,EEBEB922,85B2A20E,E6BA0D99,DE720C8C,2DA2F728,D0127845,95B794FD,647D0862,E7CCF5F0,5449A36F,877D48FA,C39DFD27,F33E8D1E,0A476341,992EFF74,3A6F6EAB,F4F8FD37,A812DC60,A1EBDDF8,991BE14C,DB6E6B0D,C67B5510,6D672C37,2765D43B,DCD0E804,F1290DC7,CC00FFA3,B5390F92,690FED0B,667B9FFB,CEDB7D9C,A091CF0B,D9155EA3,BB132F88,515BAD24,7B9479BF,763BD6EB," +
"37392EB3,CC115979,8026E297,F42E312D,6842ADA7,C66A2B3B,12754CCC,782EF11C,6A124237,B79251E7,06A1BBE6,4BFB6350,1A6B1018,11CAEDFA,3D25BDD8,E2E1C3C9,44421659,0A121386,D90CEC6E,D5ABEA2A,64AF674E,DA86A85F,BEBFE988,64E4C3FE,9DBC8057,F0F7C086,60787BF8,6003604D," +
"D1FD8346,F6381FB0,7745AE04,D736FCCC,83426B33,F01EAB71,B0804187,3C005E5F,77A057BE,BDE8AE24,55464299,BF582E61,4E58F48F,F2DDFDA2,F474EF38,8789BDC2,5366F9C3,C8B38E74,B475F255,46FCD9B9,7AEB2661,8B1DDF84,846A0E79,915F95E2,466E598E,20B45770,8CD55591,C902DE4C," +
"B90BACE1,BB8205D0,11A86248,7574A99E,B77F19B6,E0A9DC09,662D09A1,C4324633,E85A1F02,09F0BE8C,4A99A025,1D6EFE10,1AB93D1D,0BA5A4DF,A186F20F,2868F169,DCB7DA83,573906FE,A1E2CE9B,4FCD7F52,50115E01,A70683FA,A002B5C4,0DE6D027,9AF88C27,773F8641,C3604C06,61A806B5," +
"F0177A28,C0F586E0,006058AA,30DC7D62,11E69ED7,2338EA63,53C2DD94,C2C21634,BBCBEE56,90BCB6DE,EBFC7DA1,CE591D76,6F05E409,4B7C0188,39720A3D,7C927C24,86E3725F,724D9DB9,1AC15BB4,D39EB8FC,ED545578,08FCA5B5,D83D7CD3,4DAD0FC4,1E50EF5E,B161E6F8,A28514D9,6C51133C,6FD5C7E7,56E14EC4,362ABFCE,DDC6C837,D79A3234,92638212,670EFA8E,406000E0";
            string sbox4 = "3A39CE37,D3FAF5CF,ABC27737,5AC52D1B,5CB0679E,4FA33742,D3822740,99BC9BBE,D5118E9D,BF0F7315,D62D1C7E,C700C47B,B78C1B6B,21A19045,B26EB1BE,6A366EB4,5748AB2F,BC946E79,C6A376D2,6549C2C8,530FF8EE,468DDE7D,D5730A1D,4CD04DC6,2939BBDB,A9BA4650,AC9526E8,BE5EE304,A1FAD5F0,6A2D519A,63EF8CE2,9A86EE22,C089C2B8,43242EF6,A51E03AA,9CF2D0A4,83C061BA,9BE96A4D,8FE51550,BA645BD6,2826A2F9,A73A3AE1," +
"4BA99586,EF5562E9,C72FEFD3,F752F7DA,3F046F69,77FA0A59,80E4A915,87B08601,9B09E6AD,3B3EE593,E990FD5A,9E34D797,2CF0B7D9,022B8B51,96D5AC3A,017DA67D,D1CF3ED6,7C7D2D28,1F9F25CF,ADF2B89B,5AD6B472," +
"5A88F54C,E029AC71,E019A5E6,47B0ACFD,ED93FA9B,E8D3C48D,283B57CC,F8D56629,79132E28,785F0191,ED756055,F7960E44,E3D35E8C,15056DD4,88F46DBA,03A16125,0564F0BD,C3EB9E15,3C9057A2,97271AEC,A93A072A," +
"1B3F6D9B,1E6321F5,F59C66FB,26DCF319,7533D928,B155FDF5,03563482,8ABA3CBB,28517711,C20AD9F8,ABCC5167,CCAD925F,4DE81751,3830DC8E,379D5862,9320F991,EA7A90C2,FB3E7BCE,5121CE64,774FBE32,A8B6E37E,C3293D46,48DE5369,6413E680,A2AE0810,DD6DB224,69852DFD,09072166,B39A460A,6445C0DD,586CDECF,1C20C8AE,5BBEF7DD,1B588D40,CCD2017F,6BB4E3BB,DDA26A7E,3A59FF45,3E350A44,BCB4CDD5,72EACEA8,FA6484BB," +
"8D6612AE,BF3C6F47,D29BE463,542F5D9E,AEC2771B,F64E6370,740E0D8D,E75B1357,F8721671,AF537D5D,4040CB08,4EB4E2CC,34D2466A,0115AF84,E1B00428,95983A1D,06B89FB4,CE6EA048,6F3F3B82,3520AB82,011A1D4B,277227F8,611560B1,E7933FDC,BB3A792B,344525BD,A08839E1,51CE794B," +
"2F32C9B7,A01FBAC9,E01CC87E,BCC7D1F6,CF0111C3,A1E8AAC7,1A908749,D44FBD9A,D0DADECB,D50ADA38,0339C32A,C6913667,8DF9317C,E0B12B4F,F79E59B7,43F5BB3A,F2D519FF,27D9459C,BF97222C,15E6FC2A,0F91FC71,9B941525,FAE59361,CEB69CEB,C2A86459,12BAA8D1,B6C1075E,E3056A0C,10D25065,CB03A442,E0EC6E0E,1698DB3B,4C98A0BE,3278E964,9F1F9532," +
"E0D392DF,D3A0342B,8971F21E,1B0A7441,4BA3348C,C5BE7120,C37632D8,DF359F8D,9B992F2E,E60B6F47,0FE3F11D,E54CDA54,1EDAD891,CE6279CF,CD3E7E6F,1618B166,FD2C1D05,848FD2C5,F6FB2299,F523F357,A6327623,93A83531,56CCCD02,ACF08162,5A75EBB5,6E163697,88D273CC,DE966292,81B949D0,4C50901B,71C65614,E6C6C7BD,327A140A,45E1D006,C3F27B9A,C9AA53FD,62A80F00,BB25BFE2,35BDD2F6,71126905,B2040222,B6CBCF7C," +
"CD769C2B,53113EC0,1640E3D3,38ABBD60,2547ADF0,BA38209C,F746CE76,77AFA1C5,20756060,85CBFE4E,8AE88DD8,7AAAF9B0,4CF9AA7E,1948C25C,02FB8A8C,01C36AE4,D6EBE1F9,90D4F869,A65CDEA0,3F09252D,C208E69F,B74E6132,CE77E25B,578FDFE3,3AC372E6";



            string[] sbox1array = sbox1.Split(',');
            string[] sbox2array = sbox2.Split(',');
            string[] sbox3array = sbox3.Split(',');
            string[] sbox4array = sbox4.Split(',');
            byte[] sbox1ByteArray = new byte[256];
            byte[] sbox2ByteArray = new byte[256];
            byte[] sbox3ByteArray = new byte[256];
            byte[] sbox4ByteArray = new byte[256];
            for (var i = 0; i < sbox1array.Length; i++)
            {
                sbox1ByteArray[i] = (byte)Int64.Parse(sbox1array[i], System.Globalization.NumberStyles.HexNumber);
                sbox2ByteArray[i] = (byte)Int64.Parse(sbox2array[i], System.Globalization.NumberStyles.HexNumber);
                sbox3ByteArray[i] = (byte)Int64.Parse(sbox3array[i], System.Globalization.NumberStyles.HexNumber);
                sbox4ByteArray[i] = (byte)Int64.Parse(sbox4array[i], System.Globalization.NumberStyles.HexNumber);
            }

            // create bitmap object from image for the set/getpixel functions
            Bitmap image = new Bitmap(img);

            // create the byte array from the image
            byte[] byteArray = new byte[262144];
            var count = 0;
            for (var i = 0; i < image.Height; i++)
            {
                for (var j = 0; j < image.Width; j++)
                {
                    byteArray[count] = image.GetPixel(i, j).R;
                    count++;
                }
            }
            //set up final return array
            byte[] encryptionarrayFinal = new byte[262144];
            if (radioButtonCBC.Checked == true)
            {
                //count used to loop encryption until all image is done
                int loopCount = 0;
                //iterate through whole image

                while (loopCount < byteArray.Length)
                {
                    Byte[] xL = new byte[4];
                    Byte[] xR = new byte[4];
                    int tempcount = 0;
                    Byte[] cbcXOR = new byte[8];
                    //16 initial rounds of cipher   
                    for (int i = 0; i < 16; i++)
                    {
                        //create 
                        Byte[] byteTemp = new byte[8];
                        Byte[] pTemp = new byte[4];
                        //set up temp 64 bit array
                        for (int k = i * 8; k < 8 * (i + 1); k++)
                        {
                            byteTemp[tempcount] = byteArray[k];
                            tempcount++;

                        }
                        tempcount = 0;
                        if (i > 0)
                        {
                            for (int j = 0; j < byteTemp.Length; j++)
                            {
                                int tempA = byteTemp[j];
                                int tempB = cbcXOR[j];
                                int tempC = tempA ^ tempB;
                                byteTemp[j] = (Byte)tempC;
                            }

                        }
                        //partition into 32 bit left
                        for (int j = 0; j < byteTemp.Length / 2; j++)
                        {
                            xL[j] = byteTemp[j];
                        }
                        //partition into 32 bit right
                        for (int j = byteTemp.Length / 2; j < byteTemp.Length; j++)
                        {
                            xR[tempcount] = byteTemp[j];
                            tempcount++;
                        }
                        tempcount = 0;
                        //partition p into 32 bit array
                        for (int j = i * 4; j < 4 * (i + 1); j++)
                        {
                            pTemp[tempcount] = PByteFinal[j];
                            tempcount++;
                        }
                        tempcount = 0;
                        //XOR xL with P
                        for (int j = 0; j < xL.Length; j++)
                        {
                            int tempA = xL[j];
                            int tempB = pTemp[j];
                            int tempC = tempA ^ tempB;
                            xL[j] = (Byte)tempC;
                        }
                        //Function 
                        int xL1Value = xL[0];
                        int xL2Value = xL[1];
                        int xL3Value = xL[2];
                        int xL4Value = xL[3];
                        int sbox1Value = sbox1ByteArray[xL1Value];
                        int sbox2Value = sbox1ByteArray[xL2Value];
                        int sbox3Value = sbox1ByteArray[xL3Value];
                        int sbox4Value = sbox1ByteArray[xL4Value];
                        int fxL = ((((sbox1Value + sbox2Value) % (int)(Math.Pow(2, 32)) ^ sbox3Value) + sbox4Value) % (int)(Math.Pow(2, 32)));
                        byte[] bytefxL = BitConverter.GetBytes(fxL);

                        for (int j = 0; j < xR.Length; j++)
                        {

                            int tempA = bytefxL[j];
                            int tempB = xR[j];
                            int tempC = tempA ^ tempB;
                            xR[j] = (Byte)tempC;
                        }
                        //Swap xL and xR
                        byte[] temp1 = xL.ToArray();
                        xL = xR;
                        xR = temp1;


                    }
                    //Swap xL and xR back
                    byte[] temp2 = xL.ToArray();
                    xL = xR;
                    xR = temp2;
                    //initialize p17 and p18
                    byte[] p17Temp = new byte[4];
                    byte[] p18Temp = new byte[4];
                    for (int k = 64; k < 68; k++)
                    {
                        p17Temp[tempcount] = PByteFinal[k];
                    }
                    tempcount = 0;
                    for (int k = 68; k < 72; k++)
                    {
                        p18Temp[tempcount] = PByteFinal[k];
                    }
                    tempcount = 0;
                    //XOR xR with p17
                    for (int j = 0; j < xR.Length; j++)
                    {
                        int tempA = xR[j];
                        int tempB = p17Temp[j];
                        int tempC = tempA ^ tempB;
                        xR[j] = (Byte)tempC;
                    }
                    //XOR xL with p18
                    for (int j = 0; j < xL.Length; j++)
                    {
                        int tempA = xL[j];
                        int tempB = p18Temp[j];
                        int tempC = tempA ^ tempB;
                        xL[j] = (Byte)tempC;
                    }
                    //create a list out of xL and xR
                    List<byte> list1 = new List<byte>(xL);
                    List<byte> list2 = new List<byte>(xR);
                    //add xR to xL
                    list1.AddRange(list2);
                    //Store both in temp array
                    byte[] tempencryptFinal = list1.ToArray();
                    cbcXOR = list1.ToArray();
                    //create a list out of temp array and final array
                    List<byte> list3 = new List<byte>(tempencryptFinal);
                    List<byte> list4 = new List<byte>(encryptionarrayFinal);
                    //add temp array to final array
                    list4.AddRange(list3);
                    //set final array to finalarray + temp array
                    encryptionarrayFinal = list4.ToArray();
                    //loopcount+64 to get next bits for iteration
                    loopCount = loopCount + 64;
                }

            }
            else
            {
                //count used to loop encryption until all image is done
                int loopCount = 0;
                //iterate through whole image
                while (loopCount < byteArray.Length)
                {
                    Byte[] xL = new byte[4];
                    Byte[] xR = new byte[4];
                    int tempcount = 0;
                    //16 initial rounds of cipher   
                    for (int i = 0; i < 16; i++)
                    {
                        //create 
                        Byte[] byteTemp = new byte[8];
                        Byte[] pTemp = new byte[4];
                        //set up temp 64 bit array
                        for (int k = i * 8; k < 8 * (i + 1); k++)
                        {
                            byteTemp[tempcount] = byteArray[k];
                            tempcount++;

                        }
                        tempcount = 0;
                        //partition into 32 bit left
                        for (int j = 0; j < byteTemp.Length / 2; j++)
                        {
                            xL[j] = byteTemp[j];
                        }
                        //partition into 32 bit right
                        for (int j = byteTemp.Length / 2; j < byteTemp.Length; j++)
                        {
                            xR[tempcount] = byteTemp[j];
                            tempcount++;
                        }
                        tempcount = 0;
                        //partition p into 32 bit array
                        for (int j = i * 4; j < 4 * (i + 1); j++)
                        {
                            pTemp[tempcount] = PByteFinal[j];
                            tempcount++;
                        }
                        tempcount = 0;
                        //XOR xL with P
                        for (int j = 0; j < xL.Length; j++)
                        {
                            int tempA = xL[j];
                            int tempB = pTemp[j];
                            int tempC = tempA ^ tempB;
                            xL[j] = (Byte)tempC;
                        }
                        //Function 
                        int xL1Value = xL[0];
                        int xL2Value = xL[1];
                        int xL3Value = xL[2];
                        int xL4Value = xL[3];
                        int sbox1Value = sbox1ByteArray[xL1Value];
                        int sbox2Value = sbox1ByteArray[xL2Value];
                        int sbox3Value = sbox1ByteArray[xL3Value];
                        int sbox4Value = sbox1ByteArray[xL4Value];
                        int fxL = ((((sbox1Value + sbox2Value) % (int)(Math.Pow(2, 32)) ^ sbox3Value) + sbox4Value) % (int)(Math.Pow(2, 32)));
                        byte[] bytefxL = BitConverter.GetBytes(fxL);

                        for (int j = 0; j < xR.Length; j++)
                        {

                            int tempA = bytefxL[j];
                            int tempB = xR[j];
                            int tempC = tempA ^ tempB;
                            xR[j] = (Byte)tempC;
                        }
                        //Swap xL and xR
                        byte[] temp1 = xL.ToArray();
                        xL = xR;
                        xR = temp1;


                    }
                    //Swap xL and xR back
                    byte[] temp2 = xL.ToArray();
                    xL = xR;
                    xR = temp2;
                    //initialize p17 and p18
                    byte[] p17Temp = new byte[4];
                    byte[] p18Temp = new byte[4];
                    for (int k = 64; k < 68; k++)
                    {
                        p17Temp[tempcount] = PByteFinal[k];
                    }
                    tempcount = 0;
                    for (int k = 68; k < 72; k++)
                    {
                        p18Temp[tempcount] = PByteFinal[k];
                    }
                    tempcount = 0;
                    //XOR xR with p17
                    for (int j = 0; j < xR.Length; j++)
                    {
                        int tempA = xR[j];
                        int tempB = p17Temp[j];
                        int tempC = tempA ^ tempB;
                        xR[j] = (Byte)tempC;
                    }
                    //XOR xL with p18
                    for (int j = 0; j < xL.Length; j++)
                    {
                        int tempA = xL[j];
                        int tempB = p18Temp[j];
                        int tempC = tempA ^ tempB;
                        xL[j] = (Byte)tempC;
                    }
                    //create a list out of xL and xR
                    List<byte> list1 = new List<byte>(xL);
                    List<byte> list2 = new List<byte>(xR);
                    //add xR to xL
                    list1.AddRange(list2);
                    //Store both in temp array
                    byte[] tempencryptFinal = list1.ToArray();

                    //create a list out of temp array and final array
                    List<byte> list3 = new List<byte>(tempencryptFinal);
                    List<byte> list4 = new List<byte>(encryptionarrayFinal);
                    //add temp array to final array
                    list4.AddRange(list3);
                    //set final array to finalarray + temp array
                    encryptionarrayFinal = list4.ToArray();
                    //loopcount+64 to get next bits for iteration
                    loopCount = loopCount + 64;
                }

            }

            //for (var i = 0; i < encryptionarrayFinal.Length; i++)
            //{
            //    Console.WriteLine(encryptionarrayFinal[i]);
            //}
            Console.WriteLine(encryptionarrayFinal.Length);
            return encryptionarrayFinal;
        }
    }
}
