﻿using System.Drawing;
using System.Collections.Generic;
using BinaryTreeProject.App.Enums;
using BinaryTreeProject.Core.Translation;
using BinaryTreeProject.Core.Trees.BinaryTrees;
using BinaryTreeProject.Core.Trees.VisualNodes;
using BinaryTreeProject.Core.Trees.VisualTrees;
using BinaryTreeProject.Core.Trees.VisualTrees.Additions;
using BinaryTreeProject.Core.Utils;


namespace BinaryTreeProject.App.Models
{
    public class AppModel : IAppModel
    {
        private FileProvider fp;

        private VisualTree visualTree;

        private BinaryTree binTree;


        EDrawNodeMode drawNodeMode;
        EDrawTreeMode drawTreeMode;


        public AppModel(string inputFile, string outputFile, ETreeType treeType, bool agreement)
        {
            fp = new FileProvider(inputFile, outputFile);

            if (treeType == ETreeType.ShannonTree)
                binTree = new ShannonTree(fp.ProbabilitesMap);
            else
                binTree = new HaffmanTree(fp.ProbabilitesMap);

            binTree.Agreement = agreement;
            binTree.Build();

            // Set Default
            drawNodeMode = EDrawNodeMode.NotNodeMode;
            drawTreeMode = EDrawTreeMode.Nothing;

            // Because: EDrawTreeMode.Nothing
            visualTree = new VisualTree(binTree);
        }


        public void ReplaceTree(ETreeType treeType)
        {
            // Изменяется только оболочка (BinaryTree), данные остаются те же (Tree)
            if (treeType == ETreeType.ShannonTree)
                binTree = new ShannonTree(binTree);
            else
                binTree = new HaffmanTree(binTree);

            // строется дерево Шеннона/Хаффмана
            binTree.Build();
            visualTree.SetTree(binTree);
        }


        public void ReplaceAgreement(bool agreement)
        {
            binTree.Agreement = agreement;
            binTree.Build();
            visualTree.SetTree(binTree);
        }


        public void DrawTree(Graphics graph, int heigth, int width, EDrawNodeMode drawNodeMode, EDrawTreeMode drawTreeMode)
        {
            // Режим не изменился - просто рисуем под новые рамеры
            if (this.drawNodeMode == drawNodeMode && this.drawTreeMode == drawTreeMode)
                visualTree.DrawTree(width, heigth, graph);

            // Режим изменился
            else
            {
                // Изменился тип узла или тип узла и опции рисования
                if (this.drawNodeMode != drawNodeMode)
                {
                    //visualTree = new TreeWithNodes(binTree, new CircleNode(30, 5), drawTreeMode);

                    switch (drawNodeMode)
                    {
                        case EDrawNodeMode.CircleMode:
                            visualTree = new TreeWithNodes(binTree, new CircleNode(30, 5), drawTreeMode);
                            break;

                        // !!!! REWRITE !!!
                        case EDrawNodeMode.RectangleMode:
                            visualTree = new TreeWithNodes(binTree, new CircleNode(30, 5), drawTreeMode);
                            break;

                        case EDrawNodeMode.NotNodeMode:
                            visualTree = new VisualTree(binTree);
                            break;
                    }

                    visualTree.DrawTree(width, heigth, graph);
                    this.drawNodeMode = drawNodeMode;
                }
                // Изменились дополнительные опции рисования
                else
                {
                    (visualTree as TreeWithNodes)?.SetDrawMode(drawTreeMode);
                    visualTree.DrawTree(width, heigth, graph);
                    this.drawTreeMode = drawTreeMode;
                }
            }

        }

        public void PrintResult()
        {
            Converter converter = new Converter(binTree);
            fp.PrintCodes(binTree.GetBinaryCodes(), converter.LastColumnContent, 
                converter.CountRows,converter.CountColumns);
        }


        public string Encode(string originalString)
        {
            return Encoder.Encode(originalString, binTree.GetBinaryCodes());
        }

        public string Decode(string binaryString, string outputDecodeFile)
        {
            List<KeyValuePair<string, char>> decodeDetails = 
                Decoder.Decode(binaryString, binTree.GetBinaryCodes());

            fp.PrintDetailsDecoding(decodeDetails, outputDecodeFile);
            
            return Decoder.DecodeString;
        }

        public double GetValueInfo()
        {
            return binTree.GetValueInfo();
        }
    }
}