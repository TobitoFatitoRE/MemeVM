using dnlib.DotNet;
using dnlib.DotNet.Writer;

namespace MemeVM.Translation.Helpers {
    static class TokenGetter {
        internal static ModuleWriterBase Writer;

        internal static int GetMdToken(IMemberDef member) =>
            Writer.Module == member.Module ? Writer.MetaData.GetToken(member).ToInt32() : member.MDToken.ToInt32();
    }
}
