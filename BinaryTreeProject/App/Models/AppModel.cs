using System.Drawing;
using System.Collections.Generic;
using BinaryTreeProject.App.Enums;
using BinaryTreeProject.Core.IO;
using BinaryTreeProject.Core.Translation;
using BinaryTreeProject.Core.Trees.BinaryTrees;
using BinaryTreeProject.Core.Trees.VisualNodes;
using BinaryTreeProject.Core.Trees.VisualTrees;
using BinaryTreeProject.Core.Trees.VisualTrees.Additions;
using BinaryTreeProject.Core.Utils;


namespace BinaryTreeProject.App.Models
{
    /*                             Модель - описывает логику приложения                                */

    public class AppModel : IAppModel
    {
        
        //  Класс, для операций ввода-вывода
        private IO io;


        //  Экземпляр дерева для рисования
        private VisualTree visualTree;


        //  Экземпляр дерева для генерации кодовых комбинаций
        private BinaryTree binTree;


        //  Режим рисования узлов
        EDrawNodeMode drawNodeMode;


        //  Режим рисования дерева
        EDrawTreeMode drawTreeMode;


        public AppModel(string inputFile, string outputFile, ETreeType treeType, bool agreement, 
            string outputDecode, EOutputMode outputMode, char CSVseparator, bool IsTextFile)
        {
            io = new IO(inputFile, outputFile, outputDecode, outputMode, CSVseparator, IsTextFile);

            if (treeType == ETreeType.ShannonTree)
                binTree = new ShannonTree(io.ProbabilityDictionary);
            else
                binTree = new HaffmanTree(io.ProbabilityDictionary);


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
            if (treeType == ETreeType.ShannonTree)
                binTree = new ShannonTree(binTree);
            else
                binTree = new HaffmanTree(binTree);

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

                    switch (drawNodeMode)
                    {
                        case EDrawNodeMode.CircleMode:
                            visualTree = new TreeWithNodes(binTree, new CircleNode(), drawTreeMode);
                            break;

                        case EDrawNodeMode.SquareMode:
                            visualTree = new TreeWithNodes(binTree, new SquareNode(), drawTreeMode);
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
            ETreeType treeType = (binTree is ShannonTree) ? ETreeType.ShannonTree : ETreeType.HaffmanTree;

            io.PrintCodes(binTree.GetBinaryCodes(), new LastTableColumnCreator(binTree), treeType);
        }


        public string Encode(string originalString)
        {
            return Encoder.Encode(originalString, binTree.GetBinaryCodes());
        }

        public string Decode(string binaryString, string outputDecodeFile)
        {
            List<KeyValuePair<string, char>> decodeDetails = 
                Decoder.Decode(binaryString, binTree.GetBinaryCodes());

            io.PrintDetailsDecoding(decodeDetails);
            
            return Decoder.DecodeString;
        }


        public double GetValueInfo()
        {
            return binTree.GetValueInfo();
        }


        public int GetOptimalDrawingPanelHeigth()
        {
            return visualTree.GetOptimalHeigth();
        }


        public int GetOptimalDrawingPanelWidth()
        {
            return visualTree.GetOptimalWidth();
        }

    }
}