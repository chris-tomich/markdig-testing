using System;
using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace markdig_testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var document = Markdown.Parse(Resource.Sample);

            foreach (var block in document)
            {
                if (block is ContainerBlock containerBlock)
                {
                    //Console.WriteLine("ContainerBlock{" + Resource.Sample.Substring(containerBlock.Span.Start, containerBlock.Span.Length) + "}");

                    TraverseContainerBlocks(containerBlock);
                }

                if (block is LeafBlock leafBlock)
                {
                    //Console.WriteLine("LeafBlock{" + Resource.Sample.Substring(leafBlock.Span.Start, leafBlock.Span.Length) + "}");

                    TraverseLeafBlocks(leafBlock);
                }
            }
        }

        public static void TraverseContainerBlocks(ContainerBlock parentBlock)
        {
            foreach (var block in parentBlock)
            {
                if (block is ContainerBlock containerBlock)
                {
                    //Console.WriteLine("ContainerBlock{" + Resource.Sample.Substring(containerBlock.Span.Start, containerBlock.Span.Length) + "}");

                    TraverseContainerBlocks(containerBlock);
                }

                if (block is LeafBlock leafBlock)
                {
                    //Console.WriteLine("LeafBlock{" + Resource.Sample.Substring(leafBlock.Span.Start, leafBlock.Span.Length) + "}");

                    TraverseLeafBlocks(leafBlock);
                }
            }
        }

        public static void TraverseLeafBlocks(LeafBlock leafBlock)
        {
            foreach (var descendent in leafBlock.Inline.Descendants())
            {
                if (descendent is ContainerBlock containerBlock)
                {
                    //Console.WriteLine("ContainerBlock{" + Resource.Sample.Substring(containerBlock.Span.Start, containerBlock.Span.Length) + "}");

                    TraverseContainerBlocks(containerBlock);
                }

                if (descendent is LeafBlock descendentLeafBlock)
                {
                    //Console.WriteLine("LeafBlock{" + Resource.Sample.Substring(leafBlock.Span.Start, leafBlock.Span.Length) + "}");

                    TraverseLeafBlocks(descendentLeafBlock);
                }

                if (descendent is Inline inline)
                {
                    DealWithInlines(inline);
                }
            }
        }

        public static void DealWithInlines(Inline inline)
        {
            if (inline is LinkInline linkInline)
            {
                if (linkInline.LastChild is LiteralInline literalInline)
                {
                    Console.WriteLine("LinkInline{" + literalInline.Content.Text.Substring(literalInline.Content.Start, literalInline.Content.Length) + "," + linkInline.Url + "}");
                }
                else
                {
                    Console.WriteLine("LinkInline{" + linkInline.Url + "}");
                }
            }
        }
    }
}
