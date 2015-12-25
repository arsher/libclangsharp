class Example
{
public:
    Example();

    virtual bool PublicMethod() = 0;

    virtual ~Example();
private:
    void PrivateMethod();
};