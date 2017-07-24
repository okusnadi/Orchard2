﻿using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Fluid;
using Fluid.Ast;

namespace Orchard.DisplayManagement.Fluid.Statements
{
    public class DisplayStatement : Statement
    {
        public DisplayStatement(Expression shape)
        {
            Shape = shape;
        }

        public Expression Shape { get; }

        public override async Task<Completion> WriteToAsync(TextWriter writer, TextEncoder encoder, TemplateContext context)
        {
            var page = FluidViewTemplate.EnsureFluidPage(context, "Display");
            var shape = (await Shape.EvaluateAsync(context)).ToObjectValue();

            await writer.WriteAsync((await page.DisplayAsync(shape)).ToString());
            return Completion.Normal;
        }
    }
}