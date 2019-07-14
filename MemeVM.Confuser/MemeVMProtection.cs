using Confuser.Core;

namespace MemeVM.Confuser {
    [BeforeProtection("Ki.Resources", "Ki.Constants", "Ki.AntiTamper", "Ki.ControlFlow")]
    public class MemeVMProtection : Protection {
        public override string Name => "MemeVM";
        public override string Description => "Virtualization for .NET";
        public override string Id => "memevm";
        public override string FullId => "xsilent007.MemeVM";
        public override ProtectionPreset Preset => ProtectionPreset.None;

        protected override void Initialize(ConfuserContext context) { }

        protected override void PopulatePipeline(ProtectionPipeline pipeline) {
            pipeline.InsertPostStage(PipelineStage.Inspection, new InjectPhase(this));
            pipeline.InsertPreStage(PipelineStage.ProcessModule, new VirtualizatonPhase(this));
        }
    }
}
