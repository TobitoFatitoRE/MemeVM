namespace MemeVM.Runtime.Engine {
    enum VMState {
        Next,
        Exception,
        Rethrow,
        Return
    }
}
