program testconsrt;

type
  animal = class
    legs:integer;
    constructor create(legs:integer);
  end;

  dog = class(animal)
    constructor create();
  end;

var
  a:animal;

{ animal }
constructor animal.create(legs: integer);
begin
  self.legs := legs;
end;

{ dog }

constructor dog.create;
begin
  
  self.legs := 3 + inherited create(4) *4;
  inherited create(1);
  
end;

begin
   a := dog.create();
end.
